using asp_example.Commands;
using asp_example.Handlers.Commands;
using asp_example.interfaces.Commands;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_example.Config.AutoFac
{
    public class CommandHandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // TODO: Make more generic
            builder.RegisterType<AddTodoCommandHandler>().As<ICommandHandler<AddTodoCommand>>().InstancePerLifetimeScope();
        }
    }
}