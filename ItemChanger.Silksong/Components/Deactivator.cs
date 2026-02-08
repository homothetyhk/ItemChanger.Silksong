using UnityEngine;

namespace ItemChanger.Silksong.Components;

internal class Deactivator : MonoBehaviour
{
    void Start() => gameObject.SetActive(false);
}
