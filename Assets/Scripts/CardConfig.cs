using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName ="CardConfig", menuName ="Configs/CardConfig")]
internal class CardConfig : ScriptableObject
{
    public Material[] Diamond;
    public Material[] Heard;
    public Material[] Spade;
    public Material[] Club;
}

