using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUps : MonoBehaviour
{
    [SerializeField] private GameObject popUps;

    public static DamagePopUps Create(Vector3 position, string damageAmount )
    {
        Transform DamagePopUpTransform = Instantiate(GameAssets.instance.pfDamagePopUp, position, Quaternion.identity); ;
        DamagePopUps damagePopUp = DamagePopUpTransform.GetComponent<DamagePopUps>();
        damagePopUp.SetUp(damageAmount);

        return damagePopUp;
    }

    private TextMeshPro DamageText;
    private float timer =1f;
    private Color textColor;
    private static int sortingOrder;

    private void Awake()
    {
        DamageText = GetComponent<TextMeshPro>();
    }
    public void SetUp(string DamageAmount)
    {
        DamageText.SetText(DamageAmount.ToString());
        textColor = DamageText.color;
        sortingOrder++;
        DamageText.sortingOrder = sortingOrder;
        transform.rotation = Camera.main.transform.rotation;
    }

    private void Update()
    {
        float moveSpeedY = 5;
        transform.position += new Vector3(0, moveSpeedY) *Time.deltaTime;

        timer -= Time.deltaTime;
        if ( timer < 0 )
        {
            float dissapearSpeed = 4f;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            DamageText.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
