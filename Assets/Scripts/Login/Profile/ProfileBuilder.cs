using System.Collections.Generic;

namespace Barnabus.Login
{
    public class ProfileBuilder : BasePrefabBuilder
    {
        public List<ProfileController> Create(int count)
        {
            var controllers = new List<ProfileController>();
            var buildResult = base.Build(count);

            foreach (var go in buildResult)
            {
                var controller = go.GetComponent<ProfileController>();
                controller.Init();

                controllers.Add(controller);
            }

            return controllers;
        }

        public ProfileController CreateSingle()
        {
            var buildResult = base.BuildSingle();

            var controller = buildResult.GetComponent<ProfileController>();
            controller.Init();

            return controller;
        }
    }
}
 

