namespace Generate.Model
{
    using Allors.Repository.Domain;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public abstract class CompositeModel : ObjectTypeModel
    {
        protected CompositeModel(RepositoryModel repositoryModel) : base(repositoryModel) { }

        public abstract Composite Composite { get; }

        public Dictionary<string, Attribute> AttributeByName => this.Composite.AttributeByName;

        public Dictionary<string, Attribute[]> AttributesByName => this.Composite.AttributesByName;

        public XmlDoc XmlDoc => this.Composite.XmlDoc;

        public string PluralName => this.Composite.PluralName;

        public string AssignedPluralName => this.Composite.AssignedPluralName;

        public abstract InterfaceModel[] Interfaces { get; }

        public IList<InterfaceModel> ImplementedInterfaces => this.Composite.ImplementedInterfaces.Select(this.RepositoryModel.Map).ToArray();

        public PropertyModel[] Properties => this.Composite.Properties.Select(this.RepositoryModel.Map).ToArray();

        public PropertyModel[] DefinedProperties => this.Properties.Where(v => v.DefiningProperty == null).ToArray();

        public PropertyModel[] InheritedProperties => this.Properties.Where(v => v.DefiningProperty != null).ToArray();

        public PropertyModel[] DefinedReverseProperties => this.Composite.DefinedReverseProperties.Select(this.RepositoryModel.Map).ToArray();

        public PropertyModel[] InheritedReverseProperties => this.Composite.InheritedReverseProperties.Select(this.RepositoryModel.Map).ToArray();

        public MethodModel[] Methods => this.Composite.Methods.Select(this.RepositoryModel.Map).ToArray();

        public MethodModel[] DefinedMethods => this.Methods.Where(v => v.DefiningMethod == null).ToArray();

        public MethodModel[] InheritedMethods => this.Methods.Where(v => v.DefiningMethod != null).ToArray();

        public CompositeModel[] Subtypes => this.Composite.Subtypes.Select(this.RepositoryModel.Map).ToArray();

    }
}
