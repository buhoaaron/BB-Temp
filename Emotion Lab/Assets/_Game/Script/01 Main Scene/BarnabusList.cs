using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BarnabusList", order = 1)]
public class BarnabusList : ScriptableObject
{
    public List<BarnabusScanScriptable> cards;

    public BarnabusScanScriptable this[int id] { get { return cards.Find((x) => x.id == id); } }
    public int Count { get { return cards.Count; } }
}