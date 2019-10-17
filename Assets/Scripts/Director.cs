//场景控制的，负责各个场景的切换，这里只有一个场景

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Director : System.Object {
    //标志目前正在使用的场景
    public ISceneControl currentSceneControl { get; set; }
    private static Director director;
 
    private Director(){}
 
	public static Director getInstance()
    {
        if (director == null)
        {
            director = new Director();
        }
        return director;
    }
}
