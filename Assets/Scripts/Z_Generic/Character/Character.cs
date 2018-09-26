using System.Collections.Generic;
using UnityEngine;

public class Character {

    public int ID { get; set; }
    public bool IsRange { get; set; }
    public bool IsPlayer {get;set;}

	public string TempName { get; set; }

    public int Level { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Stats Stats { get; set; }

    public int Status { get; set; } 

    public Sprite Sprite { get; set; }
    public RuntimeAnimatorController Animator { get; set; }
    public Equipment Equipment { get; set; }

    public int Formation { get; set; }
    public Vector3 Position { get; set; }

    public List<Skill> Skills { get; set; }

    public Stats StatsInGame { get; set; }

    public Sprite Face { get; set; }
    public Sprite Full { get; set; }

    public List<AudioClip> Sounds { get; set; }

}
