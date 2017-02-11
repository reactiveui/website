// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace reactiveui.net.Services
{
    public interface IDeploymentEnvironment
    {
        string DeploymentId { get; }
        string RuntimeFramework { get; }

    }
}
