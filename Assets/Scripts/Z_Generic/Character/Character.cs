﻿using UnityEngine;

public class Character {

    public int ID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Stats Stats { get; set; }

    public Sprite Sprite { get; set; }
    public RuntimeAnimatorController Animator { get; set; }
    public Equipment Equipment { get; set; }

    public int Formation { get; set; }
    public Vector3 Position { get; set; }

    public Stats StatsInGame { get; set; }

}
