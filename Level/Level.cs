
using System;

public class Level
{
    public Level level;
    public SceneType[] LevelScenes { get; set; }
    public JourneyLoop CurrentLoop { get; set; }
    /// <summary>
    /// The scene(index) in the level loop
    /// </summary>
	public int CurrentLoopIndex { get; set; }
    public bool IsFinalScene { get; set; }
    public string Name { get; set; }

    public int SameLevelInRow { get; set; }

    public void InitializeGame()
    {
        this.level = new Level();
        this.LevelScenes = LevelParts.loopExploreScenes;
        //this.CurrentLoop = JourneyLoop.Beginning;
        this.IsFinalScene = false;
        this.CurrentLoopIndex = 0;
        this.Name = "The mystical journey begins!";
    }

    /// <summary>
    /// TAHAN TAIKAAA JA PALJON
    /// </summary>
	public void ChangeNewLevel()
    {
        //Scenejen sotkeminen alkaa kun tarpeeksi exp:ta?
        SceneType[] sceneArray = LevelMaker.PickLevelParts(0); //TODO
        CurrentLoopIndex++;
        GC.GetCntr().Level.LevelScenes = sceneArray;
        GC.GetCntr().Level.CurrentLoopIndex = 0; //reset the index  //0 normaalisti
        GC.GetCntr().Level.Name = "Dark Forest (Basic Journey)";
        GC.GetCntr().Level.IsFinalScene = false;
    }

}




