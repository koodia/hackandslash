

///<summary>
/// Interface which is ment for SceneBuilder classes. This interface is used when
/// a more spesific scene is needed. Everything can be set manually and just optional
/// <para>************************************************************************</para>
/// <para>PURPOSE   : Tests the scene content before it can be used in the game.  </para>
/// <para>LOGIC     : Design pattern in mind: "builder pattern"                   </para>
/// <para>COMMENTS  : 
///                   
///                                                                               </para>
/// <para>USE       : SceneBuilders                                               </para>
/// ******************************************************************************</summary>
/*******
 History   : 28/10/2016 Henry E - Created
********/
public interface ISceneBuilderAdvanced
{
    //Possiblity to set everything manually
    void InitScene();

    void SetSceneDescription();


    void Choose_BattleSpawnPoints();

    void SetSceneTypeAndScenario();

    void Build_Obstacles();

    void Build_Enemies();

    void Build_NeutralCreatures();

    void Build_GraveYard();

}