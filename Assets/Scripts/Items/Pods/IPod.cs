using System.Xml.Serialization;
using UnityEngine;

public interface IPod : IItem
{
    PodSO PodSO { get; }
    public void UsePod();
}
