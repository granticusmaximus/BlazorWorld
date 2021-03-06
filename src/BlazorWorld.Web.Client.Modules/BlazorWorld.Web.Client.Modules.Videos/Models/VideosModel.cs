﻿using BlazorWorld.Core.Helper;
using BlazorWorld.Web.Client.Modules.Common.Models;
using BlazorWorld.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace BlazorWorld.Web.Client.Modules.Videos.Models
{
    public class VideosModel : NodesModel
    {
        public VideosModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Models.Video> Items()
        {
            return Data.ConvertTo<Models.Video>();
        }
    }
}
