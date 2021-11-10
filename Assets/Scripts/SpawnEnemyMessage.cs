using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyMessage : MonoBehaviour
{
    public void SetUp()
    {
        gameObject.SetActive(true);
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(10f);

        gameObject.SetActive(false);
    }
}
