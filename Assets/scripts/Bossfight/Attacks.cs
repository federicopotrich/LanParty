using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Attacks : MonoBehaviour
{
    
    public GameObject punch, stomp, spin, idd, laser, yeet, sun;
    public Animator bossAnim;

    public IEnumerator call(int i){
        switch (i)
        {
            case 0:   yield return StartCoroutine(Spin()); break;
            case 1:   yield return StartCoroutine(Laser()); break;
            case 2:   yield return StartCoroutine(Punch()); break;
            case 3:   yield return StartCoroutine(Yeet()); break;
            case 4:   yield return StartCoroutine(IDD()); break;
            case 5:   yield return StartCoroutine(Stomp()); break;

            default: break;
        }
    }

    IEnumerator Punch(){
        GameObject punchtmp = GameObject.Instantiate(punch,new Vector3(this.transform.position.x+5,this.transform.position.y-15,0),Quaternion.identity);
        yield return new WaitForSeconds(1f);
        bossAnim.SetBool("punch",true);
        yield return new WaitForSeconds(1f);
        bossAnim.SetBool("punch",false);
        Destroy(punchtmp);
    }

    IEnumerator Stomp(){
        GameObject stomptmp;
        if((int) Random.Range(0,11)==10){
            stomptmp = GameObject.Instantiate(sun,new Vector3(this.transform.position.x+5,this.transform.position.y-5,0),Quaternion.identity);
        }else{

            stomptmp = GameObject.Instantiate(stomp,new Vector3(this.transform.position.x+5,this.transform.position.y-5,0),Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        bossAnim.SetBool("stomp",true);
        yield return new WaitForSeconds(1.5f);
        bossAnim.SetBool("stomp",false);
        Destroy(stomptmp);
    }

    IEnumerator Spin(){
        GameObject spintmp = GameObject.Instantiate(spin,new Vector3(this.transform.position.x+5,this.transform.position.y+5,0),Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        GameObject spintmp1 = GameObject.Instantiate(spin,new Vector3(this.transform.position.x+5,this.transform.position.y+5,0),Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        GameObject spintmp2 = GameObject.Instantiate(spin,new Vector3(this.transform.position.x+5,this.transform.position.y+5,0),Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        GameObject spintmp3 = GameObject.Instantiate(spin,new Vector3(this.transform.position.x+5,this.transform.position.y+5,0),Quaternion.identity);
        yield return new WaitForSeconds(1f);
        bossAnim.SetBool("spin",true);
        yield return new WaitForSeconds(1.5f);
        bossAnim.SetBool("spin",false);
        Destroy(spintmp);
        Destroy(spintmp1);
        Destroy(spintmp2);
        Destroy(spintmp3);
    }

    IEnumerator IDD(){
        
        bossAnim.SetBool("iraDegliDei",true);
        yield return new WaitForSeconds(1f);
        GameObject iddtmp = GameObject.Instantiate(idd,new Vector3(this.transform.position.x+5,this.transform.position.y,0),Quaternion.identity);
        yield return new WaitForSeconds(1.8f);
        Destroy(iddtmp);
        yield return new WaitForSeconds(1f);
        bossAnim.SetBool("iraDegliDei",false);
    }

    IEnumerator Laser(){
        bossAnim.SetBool("lazer",true);
        yield return new WaitForSeconds(1f);
        GameObject lasertmp = GameObject.Instantiate(laser,new Vector3(this.transform.position.x+7,this.transform.position.y+20,0),Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        bossAnim.SetBool("lazer",false);
        Destroy(lasertmp);
    }

    IEnumerator Yeet(){
        GameObject Yeettmp = GameObject.Instantiate(yeet,new Vector3(this.transform.position.x+20,this.transform.position.y+10,0),Quaternion.identity);
        yield return new WaitForSeconds(1f);
        bossAnim.SetBool("yeet",true);
        yield return new WaitForSeconds(1.5f);
        bossAnim.SetBool("yeet",false);
        Destroy(Yeettmp);
    }
}
