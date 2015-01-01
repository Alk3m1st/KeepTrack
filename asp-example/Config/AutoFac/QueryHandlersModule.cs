using asp_example.Handlers.Queries;
using asp_example.interfaces.Queries;
using asp_example.models.TableModels;
using asp_example.Queries;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Config.AutoFac
{
    public class QueryHandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // TODO: Make more generic
            builder.RegisterType<GetItemsByUserNameQueryHandler>().As<IQueryHandler<GetItemsByUserNameQuery, IEnumerable<TableTodo>>>().InstancePerLifetimeScope();
        }
    }
}