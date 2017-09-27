
///<summary>
/// Interface which is ment for SceneBuilder classes. One of the core interfaces.
/// Developer needs to implement both this and another "building style" interface
/// in order to create valid SceneBuilder.
/// <para>************************************************************************</para>
/// <para>PURPOSE   : The bare minimum requirements for building a scene          </para>
/// <para>LOGIC     : Design pattern in mind: "builder pattern"                   </para>
/// <para>COMMENTS  : 
///                   
///                                                                               </para>
/// <para>USE       : SceneBuilders                                               </para>
/// ******************************************************************************</summary>
/*******
 History   : 28/10/2016 Henry E - Created
********/
public interface ISceneBuilder
{
    void QuickBuild();

    void SetStageCameraPosition();

    void Build_Stage();

    void SetMusic();

    Scene GetScene();
}