// <copyright file="Program.cs" company="Allors bvba">
// Copyright (c) Allors bvba. All rights reserved.
// Licensed under the LGPL license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Allors.Meta.Generation.Storage
{
    using System;
    using System.IO;
    using Allors.Database.Meta.Configuration;
    using Allors.Database.Meta;
    using Allors.Meta.Generation.Model;

    internal class Program
    {
        private static readonly MetaBuilder MetaBuilder = new MetaBuilder();

        private static int Main()
        {
            string[,] database =
            {
                { "../Core/Database/Templates/domain.cs.stg", "Database/Domain/Generated" },
                { "../Core/Database/Templates/uml.cs.stg", "Database/Domain.Diagrams/Generated" },
            };

            string[,] workspace =
            {
                { "../Core/Workspace/Templates/meta.cs.stg", "Workspace/Meta/Generated" },
                //{ "../Core/Workspace/Templates/domain.cs.stg", "Workspace/Domain/Generated" },
                { "../Core/Workspace/Templates/uml.cs.stg", "Workspace/Domain.Diagrams/Generated" },

                { "../../typescript/templates/workspace.meta.ts.stg", "../../typescript/libs/base/workspace/meta/src/lib/generated" },
                { "../../typescript/templates/workspace.meta.json.ts.stg", "../../typescript/libs/base/workspace/meta-json/src/lib/generated" },
                { "../../typescript/templates/workspace.domain.ts.stg", "../../typescript/libs/base/workspace/domain/src/lib/generated" },
            };

            var metaPopulation = MetaBuilder.Build();
            var model = new MetaModel(metaPopulation);

            for (var i = 0; i < database.GetLength(0); i++)
            {
                var template = database[i, 0];
                var output = database[i, 1];

                Console.WriteLine("-> " + output);

                RemoveDirectory(output);

                var log = Generate.Execute(model, template, output);
                if (log.ErrorOccured)
                {
                    return 1;
                }
            }

            var workspaceName = "Default";

            for (var i = 0; i < workspace.GetLength(0); i++)
            {
                var template = workspace[i, 0];
                var output = workspace[i, 1];

                Console.WriteLine("-> " + output);

                RemoveDirectory(output);

                var log = Generate.Execute(model, template, output, workspaceName);
                if (log.ErrorOccured)
                {
                    return 1;
                }
            }

            return 0;
        }

        private static void RemoveDirectory(string output)
        {
            var directoryInfo = new DirectoryInfo(output);
            if (directoryInfo.Exists)
            {
                try
                {
                    directoryInfo.Delete(true);
                }
                catch
                {
                }
            }
        }
    }
}
