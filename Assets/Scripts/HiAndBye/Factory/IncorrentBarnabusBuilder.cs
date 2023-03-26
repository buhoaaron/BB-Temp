using System.Collections.Generic;
using UnityEngine;

namespace HiAndBye
{
    public class IncorrentBarnabusBuilder : BasePrefabBuilder
    {
        public List<IncorrentBarnabusController> Create(int count)
        {
            var controllers = new List<IncorrentBarnabusController>();
            var buildResult = base.Build(count);

            foreach (var go in buildResult)
            {
                var controller = new IncorrentBarnabusController(go);
                controller.Init();

                controllers.Add(controller);
            }

            return controllers;
        }
    }
}
 

