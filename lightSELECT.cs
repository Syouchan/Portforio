using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSELECT : MonoBehaviour
{
    public GameObject[] selectlight;
    private bool ONOFF = true;
    private float scroll;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            ONOFF = !false;
            Debug.Log(ONOFF);
        }
        if (ONOFF == true)
        {
            Select();
        }

    }
    private void Select()
    {
        //�}�E�X�z�C�[���̉��]�̎擾�������l��0.1�ׂ̈�10
        scroll = Input.GetAxis("Mouse ScrollWheel") * 10;
        //�}�E�X�z�C�[���𓮂������Ƃ�
        if (scroll != 0 && Input.GetKey(KeyCode.R))
        {
            //�I���A�C�e���̕ύX
            if (i < selectlight.Length - 1 && i > 0)
            {
                if (scroll > 0) i++;
                else i--;
            }
            else if (i <= 0 && scroll <= 0) i = selectlight.Length - 1;

            else if (i == 0 && scroll > 0) i += (int)scroll;

            else if (i == selectlight.Length - 1 && scroll < 0) i -= 1;

            else i = 0;
            //�Z���N�^�[�̃A�N�e�B�u���Ԃ̕ύX
            for (int j = 0; j < selectlight.Length; j++)
            {
                selectlight[j].SetActive(false);
            }
            selectlight[i].SetActive(true);
            scroll = 0;
        }
    }
}
