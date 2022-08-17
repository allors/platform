// <copyright file="RecordBasedOriginState.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Workspace.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Meta;
    using Shared.Ranges;

    public abstract class RecordBasedOriginState
    {
        public abstract Strategy Strategy { get; }

        protected bool HasChanges => this.Record == null || this.ChangedRoleByRelationType?.Count > 0;

        protected abstract IEnumerable<RoleType> RoleTypes { get; }

        protected abstract IRecord Record { get; }

        protected IRecord PreviousRecord { get; set; }

        public Dictionary<RelationType, object> ChangedRoleByRelationType { get; private set; }

        private Dictionary<RelationType, object> PreviousChangedRoleByRelationType { get; set; }

        public object GetUnitRole(RoleType roleType)
        {
            if (this.ChangedRoleByRelationType != null && this.ChangedRoleByRelationType.TryGetValue(roleType.RelationType, out var role))
            {
                return role;
            }

            return this.Record?.GetRole(roleType);
        }

        public void SetUnitRole(RoleType roleType, object role) => this.SetChangedRole(roleType, role);

        public Strategy GetCompositeRole(RoleType roleType)
        {
            if (this.ChangedRoleByRelationType != null && this.ChangedRoleByRelationType.TryGetValue(roleType.RelationType, out var changedRole))
            {
                return (Strategy)changedRole;
            }

            var role = this.Record?.GetRole(roleType);

            if (role == null)
            {
                return null;
            }

            var strategy = this.Session.GetStrategy((long)role);
            this.AssertStrategy(strategy);
            return strategy;
        }

        public void SetCompositeRole(RoleType roleType, Strategy role)
        {
            if (this.SameCompositeRole(roleType, role))
            {
                return;
            }

            var associationType = roleType.AssociationType;
            if (associationType.IsOne && role != null)
            {
                var previousAssociation = this.Session.GetCompositeAssociation(role, associationType);
                this.SetChangedRole(roleType, role);
                if (associationType.IsOne && previousAssociation != null)
                {
                    // OneToOne
                    previousAssociation.SetRole(roleType, null);
                }
            }
            else
            {
                this.SetChangedRole(roleType, role);
            }
        }

        public RefRange<Strategy> GetCompositesRole(RoleType roleType)
        {
            if (this.ChangedRoleByRelationType != null && this.ChangedRoleByRelationType.TryGetValue(roleType.RelationType, out var changedRole))
            {
                return (RefRange<Strategy>)changedRole;
            }

            var role = (ValueRange<long>)(this.Record?.GetRole(roleType) ?? ValueRange<long>.Empty);

            if (role.IsEmpty)
            {
                return RefRange<Strategy>.Empty;
            }

            return RefRange<Strategy>.Load(role.Select(v =>
            {
                var strategy = this.Session.GetStrategy(v);
                this.AssertStrategy(strategy);
                return strategy;
            }));
        }

        public void AddCompositesRole(RoleType roleType, Strategy roleToAdd)
        {
            var associationType = roleType.AssociationType;
            var previousAssociation = this.Session.GetCompositeAssociation(roleToAdd, associationType);

            var role = this.GetCompositesRole(roleType);

            if (role.Contains(roleToAdd))
            {
                return;
            }

            role = role.Add(roleToAdd);
            this.SetChangedRole(roleType, role);

            if (associationType.IsMany)
            {
                return;
            }

            // OneToMany
            previousAssociation?.SetRole(roleType, null);
        }

        public void RemoveCompositesRole(RoleType roleType, Strategy roleToRemove)
        {
            var role = this.GetCompositesRole(roleType);

            if (!role.Contains(roleToRemove))
            {
                return;
            }

            role = role.Remove(roleToRemove);
            this.SetChangedRole(roleType, role);
        }

        public void SetCompositesRole(RoleType roleType, RefRange<Strategy> role)
        {
            if (this.SameCompositesRole(roleType, role))
            {
                return;
            }

            var previousRole = this.GetCompositesRoleIfInstantiated(roleType);

            this.SetChangedRole(roleType, role);

            var associationType = roleType.AssociationType;
            if (associationType.IsMany)
            {
                return;
            }

            // OneToMany
            foreach (var addedRole in role.Except(previousRole))
            {
                var previousAssociation = this.Session.GetCompositeAssociation(addedRole, associationType);
                previousAssociation?.SetRole(roleType, null);
            }
        }

        public void Checkpoint(ChangeSet changeSet)
        {
            // Same record
            if (this.PreviousRecord == null || this.Record == null || this.Record.Version == this.PreviousRecord.Version)
            {
                foreach (var kvp in this.ChangedRoleByRelationType)
                {
                    var relationType = kvp.Key;
                    var current = kvp.Value;

                    if (this.PreviousChangedRoleByRelationType != null && this.PreviousChangedRoleByRelationType.TryGetValue(relationType, out var previousChangedRole))
                    {
                        if (relationType.RoleType.ObjectType.IsUnit)
                        {
                            changeSet.DiffUnit(this.Strategy, relationType, current, previousChangedRole);
                        }
                        else if (relationType.RoleType.IsOne)
                        {
                            changeSet.DiffComposite(this.Strategy, relationType, (Strategy)current, (Strategy)previousChangedRole);
                        }
                        else
                        {
                            changeSet.DiffComposites(this.Strategy, relationType, (RefRange<Strategy>)current, (RefRange<Strategy>)previousChangedRole);
                        }
                    }
                    else
                    {
                        var previous = this.Record?.GetRole(relationType.RoleType);

                        if (relationType.RoleType.ObjectType.IsUnit)
                        {
                            changeSet.DiffUnit(this.Strategy, relationType, current, previous);
                        }
                        else if (relationType.RoleType.IsOne)
                        {
                            changeSet.DiffComposite(this.Strategy, relationType, (Strategy)current, (long?)previous);
                        }
                        else
                        {
                            changeSet.DiffComposites(this.Strategy, relationType, (RefRange<Strategy>)current, (ValueRange<long>)(previous ?? ValueRange<long>.Empty));
                        }

                    }
                }
            }
            // Different record
            else
            {
                foreach (var roleType in this.RoleTypes)
                {
                    var relationType = roleType.RelationType;

                    object previous = null;
                    object current = null;

                    if (this.PreviousChangedRoleByRelationType?.TryGetValue(relationType, out previous) == true)
                    {
                        if (this.ChangedRoleByRelationType?.TryGetValue(relationType, out current) != true)
                        {
                            current = this.Record.GetRole(roleType);
                        }

                        if (relationType.RoleType.ObjectType.IsUnit)
                        {
                            changeSet.DiffUnit(this.Strategy, relationType, current, previous);
                        }
                        else if (relationType.RoleType.IsOne)
                        {
                            changeSet.DiffComposite(this.Strategy, relationType, (Strategy)current, (Strategy)previous);
                        }
                        else
                        {
                            changeSet.DiffComposites(this.Strategy, relationType, (RefRange<Strategy>)current, (RefRange<Strategy>)previous);
                        }
                    }
                    else
                    {
                        previous = this.PreviousRecord?.GetRole(roleType);
                        if (this.ChangedRoleByRelationType?.TryGetValue(relationType, out current) != true)
                        {
                            current = this.Record.GetRole(roleType);
                        }

                        if (relationType.RoleType.ObjectType.IsUnit)
                        {
                            changeSet.DiffUnit(this.Strategy, relationType, current, previous);
                        }
                        else if (relationType.RoleType.IsOne)
                        {
                            changeSet.DiffComposite(this.Strategy, relationType, (long?)current, (long?)previous);
                        }
                        else
                        {
                            changeSet.DiffComposites(this.Strategy, relationType, (ValueRange<long>)(current ?? ValueRange<long>.Empty), (ValueRange<long>)previous);
                        }
                    }
                }
            }

            this.PreviousRecord = this.Record;
            this.PreviousChangedRoleByRelationType = this.ChangedRoleByRelationType != null ? new Dictionary<RelationType, object>(this.ChangedRoleByRelationType) : null;
        }

        public void Diff(IList<IDiff> diffs)
        {
            if (this.ChangedRoleByRelationType == null)
            {
                return;
            }

            foreach (var kvp in this.ChangedRoleByRelationType)
            {
                var relationType = kvp.Key;
                var roleType = relationType.RoleType;

                var changed = kvp.Value;
                var original = this.Record?.GetRole(roleType);

                if (roleType.ObjectType.IsUnit)
                {
                    diffs.Add(new UnitDiff(relationType, this.Strategy)
                    {
                        OriginalRole = original,
                        ChangedRole = changed,
                    });
                }
                else if (roleType.IsOne)
                {
                    diffs.Add(new CompositeDiff(relationType, this.Strategy)
                    {
                        OriginalRole = original != null ? this.Session.GetStrategy((long)original) : null,
                        ChangedRole = (Strategy)changed,
                    });
                }
                else
                {
                    diffs.Add(new CompositesDiff(relationType, this.Strategy)
                    {
                        OriginalRoles = original != null ? ValueRange<long>.Load((IEnumerable<long>)original).Select(v => this.Session.GetStrategy(v)).ToArray() : Array.Empty<Strategy>(),
                        ChangedRoles = ((RefRange<Strategy>)changed).Save() ?? Array.Empty<Strategy>(),
                    });
                }
            }
        }

        public bool CanMerge(IRecord newRecord)
        {
            if (this.ChangedRoleByRelationType == null)
            {
                return true;
            }

            foreach (var kvp in this.ChangedRoleByRelationType)
            {
                var relationType = kvp.Key;
                var roleType = relationType.RoleType;

                var original = this.Record?.GetRole(roleType);
                var newOriginal = newRecord?.GetRole(roleType);

                if (roleType.ObjectType.IsUnit)
                {
                    if (!Equals(original, newOriginal))
                    {
                        return false;
                    }
                }
                else if (roleType.IsOne)
                {
                    if (!Equals(original, newOriginal))
                    {
                        return false;
                    }
                }
                else if (!ValueRange<long>.Load((IEnumerable<long>)original).Equals(ValueRange<long>.Load((IEnumerable<long>)newOriginal)))
                {
                    return false;
                }
            }

            return true;
        }

        public bool HashChanges() => this.ChangedRoleByRelationType != null;

        public void Reset() => this.ChangedRoleByRelationType = null;

        public bool IsAssociationForRole(RoleType roleType, Strategy forRole)
        {
            if (roleType.IsOne)
            {
                var compositeRole = this.GetCompositeRoleIfInstantiated(roleType);
                return compositeRole == forRole;
            }

            var compositesRole = this.GetCompositesRoleIfInstantiated(roleType);
            return compositesRole.Contains(forRole);
        }

        public bool HasChanged(RoleType roleType) => this.ChangedRoleByRelationType?.ContainsKey(roleType.RelationType) ?? false;

        public void RestoreRole(RoleType roleType) => this.ChangedRoleByRelationType?.Remove(roleType.RelationType);

        protected abstract void OnChange();

        private void SetChangedRole(RoleType roleType, object role)
        {
            this.ChangedRoleByRelationType ??= new Dictionary<RelationType, object>();
            this.ChangedRoleByRelationType[roleType.RelationType] = role;
            this.OnChange();
        }

        private Strategy GetCompositeRoleIfInstantiated(RoleType roleType)
        {
            if (this.ChangedRoleByRelationType != null && this.ChangedRoleByRelationType.TryGetValue(roleType.RelationType, out var changedRole))
            {
                return (Strategy)changedRole;
            }

            var role = this.Record?.GetRole(roleType);
            return role == null ? null : this.Session.GetStrategy((long)role);
        }

        private RefRange<Strategy> GetCompositesRoleIfInstantiated(RoleType roleType)
        {
            if (this.ChangedRoleByRelationType != null && this.ChangedRoleByRelationType.TryGetValue(roleType.RelationType, out var changedRole))
            {
                return (RefRange<Strategy>)changedRole;
            }

            var role = (ValueRange<long>)(this.Record?.GetRole(roleType) ?? ValueRange<long>.Empty);
            return role.IsEmpty ? RefRange<Strategy>.Empty : RefRange<Strategy>.Load(role.Select(v => this.Session.GetStrategy(v)).Where(v => v != null));
        }
        private bool SameCompositeRole(RoleType roleType, Strategy role)
        {
            if (this.ChangedRoleByRelationType != null && this.ChangedRoleByRelationType.TryGetValue(roleType.RelationType, out var changedRole))
            {
                return role == changedRole;
            }

            var changedRoleId = this.Record?.GetRole(roleType);

            if (role == null)
            {
                return changedRoleId == null;
            }

            if (changedRoleId == null)
            {
                return false;
            }

            return role.Id == (long)changedRoleId;
        }

        private bool SameCompositesRole(RoleType roleType, RefRange<Strategy> role)
        {
            if (this.ChangedRoleByRelationType != null && this.ChangedRoleByRelationType.TryGetValue(roleType.RelationType, out var changedRole))
            {
                return role.Equals(changedRole);
            }

            var roleIds = ValueRange<long>.Load((IEnumerable<long>)this.Record?.GetRole(roleType));
            return role.IsEmpty ? roleIds.IsEmpty : role.Select(v => v.Id).SequenceEqual(roleIds);
        }

        private void AssertStrategy(Strategy strategy)
        {
            if (strategy == null)
            {
                throw new Exception("Strategy is not in Session.");
            }
        }

        #region Proxy Properties

        protected long Id => this.Strategy.Id;

        protected Class Class => this.Strategy.Class;

        protected Session Session => this.Strategy.Session;

        protected Workspace Workspace => this.Session.Workspace;

        #endregion
    }
}
