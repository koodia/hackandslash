using UnityEngine;
using System;

public class SceneEngineer
{
    private ISceneBuilder sceneBuilder;

    public SceneEngineer(ISceneBuilder bluePrint)
    {
        this.sceneBuilder = bluePrint;
    }

    public Scene GetScene()
    {
        return this.sceneBuilder.GetScene();
    }

    public void MakeScene()
    {
        this.sceneBuilder.QuickBuild();
    }
}



