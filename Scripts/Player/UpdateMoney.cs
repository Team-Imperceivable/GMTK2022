using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private Text moneyText;

    private void Start()
    {
        moneyText = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        moneyText.text = playerController.money.ToString();
    }
}
