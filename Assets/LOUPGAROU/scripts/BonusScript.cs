using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    public float delayToUse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GetBonusEffect()
    {
        yield return delayToUse;

        GameManager_LG2.Instance.Heal();
        Destroy(gameObject);
    }

    public void GetBonusEffectInstant()
    {
        GameManager_LG2.Instance.Heal();
        Destroy(gameObject);
    }
}
