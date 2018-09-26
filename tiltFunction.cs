using UnityEngine;
using System.Collections;


public class tiltFunction : MonoBehaviour {

	private float speed =   0f;
	public float minPos =  15f;
	public float maxPos = -12f;
	public float ypos = 0;
	public float xpos = 0;
	private float hiz = 0f;
	private Vector3 dir;
	private bool isRunning = false;
	private float timeBack = 3.9F;
	public static int score = 0;
	public int TextWidth = 180;
	public GUIStyle style;
	public string pScore;
	public GameObject TotalScoreEN;
	public GameObject TotalScoreTR;
	public GameObject scoreBx;
	public AudioClip[] Clips;
	public AudioSource[] audioSources;
	public int timeBackInt;
	public int randomNumber;
	public int luckyPlayer;
	public Vector3 fishCmn;
	public int randomJump;
	public int randomAnim;
	public int saveNumber = 0;
	public GameObject trackAnimation;
	public bool checkTrackAnim = false;
	public float slideSpeed = 300f;
	public bool trackAnimRun = true;

	void Start(){

		trackAnimation = GameObject.Find("trckAnim");

				audioSources = new AudioSource[Clips.Length];
				int i = 0;
				while (i < Clips.Length){

				GameObject child = new GameObject("Player");

				child.transform.parent = gameObject.transform;
				child.transform.position = gameObject.transform.position;
				audioSources[i] = child.AddComponent("AudioSource") as AudioSource;
				audioSources[i].clip = Clips[i];
				i++;
			}

			scoreBx = GameObject.FindGameObjectWithTag("scoreB");

				audioSources[0].Play();
				audioSources[0].audio.loop = true;
				audioSources[7].audio.volume = 0.3F;
				audioSources[8].Play();
				audioSources[8].audio.loop = true;
				audioSources[8].audio.volume = 0.1F;
				score = 0;
				Time.timeScale = 1;
	}

			
	void FixedUpdate(){

		transform.position -= transform.TransformDirection(Vector3.forward * Time.deltaTime * hiz);	 
		rigidbody.angularVelocity = Vector3.Lerp(rigidbody.angularVelocity, Vector3.zero, Time.deltaTime *30f);
		rigidbody.velocity = new Vector3( rigidbody.velocity.x ,rigidbody.velocity.y , 0 );

	}
	
	void Update() {

        //Snowboard kar izi
        
		if(trackAnimRun == true){
		trackAnimation.transform.renderer.material.mainTextureOffset -= new Vector2  ( Time.deltaTime * slideSpeed,0);
        }	
        
        //animasyonların dieAnimi etkilememesi gerekiyor. Oyunun sonlanması için güvenlik amaçlı diğer animasyonları kontrol ediyor.
        
		while(animation.IsPlaying("dieAnim")){
				animation.Stop("extremeAnim_1"); animation.Stop("extremeAnim_2"); animation.Stop("extremeAnim_3");	animation.Stop("extremeAnim_4"); animation.Stop("extremeAnim_5");
				animation.Stop("extremeAnim_6"); animation.Stop("extremeAnim_7"); animation.Stop("extremeAnim_8"); 	animation.Stop("extremeAnim_9"); animation.Stop("extremeAnim_10");
				animation.Stop("extremeAnim_11"); 	animation.Stop("extremeAnim_12"); animation.Stop("extremeAnim_13"); animation.Stop("extremeAnim_14"); animation.Stop("extremeAnim_15");
				animation.Stop("extremeAnim_16"); animation.Stop("extremeAnim_17"); animation.Stop("extremeAnim_18"); animation.Stop("animLeft"); animation.Stop("animRight");
				animation.Stop("jump_1"); animation.Stop("jump_2"); animation.Stop("jump_3");
			return;
			
		}
	
        // TimeBack Int'e çevir
        
		if(timeBack > -1){		
			timeBackInt = Mathf.FloorToInt(timeBack); 
		}	   
        
		TimeBackFunc();
		NumberOfSound();
	
		// Mathf.Clamp ile Yön Kısıtlamaları
        
		Vector3 pos = transform.position;
		pos.x =  Mathf.Clamp(transform.position.x, -5.55f, 5.25f);
		transform.position = pos;
		
        //------------------
        
		dir = Vector3.zero;
		dir.x = -Input.acceleration.x;
		dir.z = 0;
		if (dir.sqrMagnitude > 1)
			dir.Normalize();
			dir *= Time.deltaTime;
			transform.Translate(dir * speed);

		//Debug.Log(dir.x);
		
		if(Input.touchCount==0&dir.x>0&isRunning==false){
			leftAnimation();
		}
		
		else if(Input.touchCount==0&dir.x<0&isRunning==false){
			rightAnimation();
		}		
		
		if(transform.localPosition.x>minPos){
			
			transform.localPosition = new Vector3 (minPos,transform.localPosition.y,transform.localPosition.z);
		}
		
		else if(transform.localPosition.x<maxPos){
			
			transform.localPosition = new Vector3 (maxPos,transform.localPosition.y,transform.localPosition.z);
		}
	}

	void AdSave(){

		//Reklam Tanımlamaları
        
        saveNumber = PlayerPrefs.GetInt("saveNumber"); 
		PlayerPrefs.SetInt("saveNumber", (PlayerPrefs.GetInt("saveNumber")+1));
		PlayerPrefs.Save();
		Debug.Log(saveNumber);
		if(saveNumber == 3){
			Admob_Interstitial.jo.Call("showInterstitial");
			PlayerPrefs.SetInt("saveNumber", (PlayerPrefs.GetInt("saveNumber")-4));
			PlayerPrefs.Save();
			}
		}

	void NumberOfSound(){
        
        // Ses Tanımlamaları

		if(timeBackInt == 3){
			if(audioSources[2].isPlaying == false){
				audioSources[2].Play();
			}			
		}		
		if(timeBackInt == 2){
			if(audioSources[3].isPlaying == false){
				audioSources[3].Play();
			}			
		}		
		if(timeBackInt == 1){
			if(audioSources[4].isPlaying == false){
				audioSources[4].Play();
			}
		}		
		if(timeBackInt == 0){
			if(audioSources[5].isPlaying == false){
				audioSources[5].Play();
			}
		}

	}

	void TimeBackFunc(){

		// Timeback - Geri Sayım -
		
		if(timeBack > 0){
			timeBack -= Time.deltaTime ;
			animation.Stop();
		}
		
		if(timeBack <= 0){
			animation.Play();
			hiz = 20;
			speed = 25f;
			checkTrackAnim = true;
		}
		//--------------------

	}

	void jumpComing(){
        
        // Zıplama Animasyonları 3 animasyon arasından random olarak çağırıyorum.
        
		trackAnimation.animation.Stop();
		trackAnimation.renderer.enabled =false;
		randomJump = Random.Range(1,4);

		switch(randomJump){

		case(1): StartCoroutine(jumpAnim1()); break;
		case(2): StartCoroutine(jumpAnim2()); break;
		case(3): StartCoroutine(jumpAnim3()); break;
		}
	}

	void animComing(){
        
        // Rampadan çıkma - extreme animasyonları çağırma
					
		randomAnim = Random.Range(1,19);
				
		switch(randomAnim){
			
		case(1): StartCoroutine(extremeAnimation1()); 		break;
		case(2): StartCoroutine(extremeAnimation2());		break;
		case(3): StartCoroutine(extremeAnimation3());		break;
		case(4): StartCoroutine(extremeAnimation4());		break;
		case(5): StartCoroutine(extremeAnimation5());		break;
		case(6): StartCoroutine(extremeAnimation6()); 		break;
		case(7): StartCoroutine(extremeAnimation7());		break;
		case(8): StartCoroutine(extremeAnimation8());		break;
		case(9): StartCoroutine(extremeAnimation9());		break;
		case(10):StartCoroutine(extremeAnimation10());		break;
		case(11):StartCoroutine(extremeAnimation11());		break;
		case(12):StartCoroutine(extremeAnimation12());		break;
		case(13):StartCoroutine(extremeAnimation13());		break;
		case(14):StartCoroutine(extremeAnimation14());		break;
		case(15):StartCoroutine(extremeAnimation15());		break;
		case(16):StartCoroutine(extremeAnimation16());		break;
		case(17):StartCoroutine(extremeAnimation17());		break;
		case(18):StartCoroutine(extremeAnimation18());		break;
		}

	}
    
    // Yön ve Hareket Animasyonlarının aktivasyonu

 void leftAnimation(){
		isRunning=true;
		animation["animLeft"].layer = 5;
		animation["animLeft"].wrapMode = WrapMode.ClampForever;
		animation["animLeft"].speed = 3;
		animation.CrossFade("animLeft");
		//yield return new WaitForSeconds(animation["left"].clip.length);
		isRunning=false;
	}
	
 void rightAnimation(){
		isRunning=true;
		animation["animRight"].layer = 5;
		animation["animRight"].wrapMode = WrapMode.ClampForever;
		animation["animRight"].speed = 3;
		animation.CrossFade("animRight");
		//yield return new WaitForSeconds(animation["right"].clip.length);
		isRunning=false;
		
	}

	IEnumerator jumpAnim1(){
		audioSources[6].Play();
		isRunning=true;
		animation["jump_1"].layer = 5;
		animation["jump_1"].wrapMode = WrapMode.ClampForever;
		animation["jump_1"].speed = 1;
		animation.Play("jump_1");
		yield return new WaitForSeconds(animation["jump_1"].clip.length);
		isRunning=false;
	}

	IEnumerator jumpAnim2(){
		audioSources[6].Play();
		isRunning=true;
		animation["jump_2"].layer = 5;
		animation["jump_2"].wrapMode = WrapMode.ClampForever;
		animation["jump_2"].speed = 1;
		animation.Play("jump_2");
		yield return new WaitForSeconds(animation["jump_2"].clip.length);
		isRunning=false;
	}

	IEnumerator jumpAnim3(){
		audioSources[6].Play();
		isRunning=true;
		animation["jump_3"].layer = 5;
		animation["jump_3"].wrapMode = WrapMode.ClampForever;
		animation["jump_3"].speed = 1;
		animation.Play("jump_3");
		yield return new WaitForSeconds(animation["jump_3"].clip.length);
		isRunning=false;
	}


	IEnumerator extremeAnimation1(){
		
		
		isRunning=true;
		animation["extremeAnim_1"].layer = 5;
		animation["extremeAnim_1"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_1"].speed = 1;
		animation.Play("extremeAnim_1");
		yield return new WaitForSeconds(animation["extremeAnim_1"].clip.length);
		isRunning=false;
		
		
	}

	IEnumerator extremeAnimation2(){
		
		
		isRunning=true;
		animation["extremeAnim_2"].layer = 5;
		animation["extremeAnim_2"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_2"].speed = 1;
		animation.Play("extremeAnim_2");
		yield return new WaitForSeconds(animation["extremeAnim_2"].clip.length);
		isRunning=false;
		
		
	}

	IEnumerator extremeAnimation3(){
		
		
		isRunning=true;
		animation["extremeAnim_3"].layer = 5;
		animation["extremeAnim_3"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_3"].speed = 1;
		animation.Play("extremeAnim_3");
		yield return new WaitForSeconds(animation["extremeAnim_3"].clip.length);
		isRunning=false;
		
		
	}

	IEnumerator extremeAnimation4(){
		
		
		isRunning=true;
		animation["extremeAnim_4"].layer = 5;
		animation["extremeAnim_4"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_4"].speed = 1;
		animation.Play("extremeAnim_4");
		yield return new WaitForSeconds(animation["extremeAnim_4"].clip.length);
		isRunning=false;
		
		
	}

	IEnumerator extremeAnimation5(){
		
		
		isRunning=true;
		animation["extremeAnim_5"].layer = 5;
		animation["extremeAnim_5"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_5"].speed = 1;
		animation.Play("extremeAnim_5");
		yield return new WaitForSeconds(animation["extremeAnim_5"].clip.length);
		isRunning=false;
		
		
	}

	IEnumerator extremeAnimation6(){
		
		
		isRunning=true;
		animation["extremeAnim_6"].layer = 5;
		animation["extremeAnim_6"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_6"].speed = 1;
		animation.Play("extremeAnim_6");
		yield return new WaitForSeconds(animation["extremeAnim_6"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation7(){
		
		
		isRunning=true;
		animation["extremeAnim_7"].layer = 5;
		animation["extremeAnim_7"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_7"].speed = 1;
		animation.Play("extremeAnim_7");
		yield return new WaitForSeconds(animation["extremeAnim_7"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation8(){
		
		
		isRunning=true;
		animation["extremeAnim_8"].layer = 5;
		animation["extremeAnim_8"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_8"].speed = 1;
		animation.Play("extremeAnim_8");
		yield return new WaitForSeconds(animation["extremeAnim_8"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation9(){
		
		
		isRunning=true;
		animation["extremeAnim_9"].layer = 5;
		animation["extremeAnim_9"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_9"].speed = 1;
		animation.Play("extremeAnim_9");
		yield return new WaitForSeconds(animation["extremeAnim_9"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation10(){
		
		
		isRunning=true;
		animation["extremeAnim_10"].layer = 5;
		animation["extremeAnim_10"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_10"].speed = 1;
		animation.Play("extremeAnim_10");
		yield return new WaitForSeconds(animation["extremeAnim_10"].clip.length);
		isRunning=false;
		
		
	}

	IEnumerator extremeAnimation11(){
		
		
		isRunning=true;
		animation["extremeAnim_11"].layer = 5;
		animation["extremeAnim_11"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_11"].speed = 1;
		animation.Play("extremeAnim_11");
		yield return new WaitForSeconds(animation["extremeAnim_11"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation12(){
		
		
		isRunning=true;
		animation["extremeAnim_12"].layer = 5;
		animation["extremeAnim_12"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_12"].speed = 1;
		animation.Play("extremeAnim_12");
		yield return new WaitForSeconds(animation["extremeAnim_12"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation13(){
		
		
		isRunning=true;
		animation["extremeAnim_13"].layer = 5;
		animation["extremeAnim_13"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_13"].speed = 1;
		animation.Play("extremeAnim_13");
		yield return new WaitForSeconds(animation["extremeAnim_13"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation14(){
		
		
		isRunning=true;
		animation["extremeAnim_14"].layer = 5;
		animation["extremeAnim_14"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_14"].speed = 1;
		animation.Play("extremeAnim_14");
		yield return new WaitForSeconds(animation["extremeAnim_14"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation15(){
		
		
		isRunning=true;
		animation["extremeAnim_15"].layer = 5;
		animation["extremeAnim_15"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_15"].speed = 1;
		animation.Play("extremeAnim_15");
		yield return new WaitForSeconds(animation["extremeAnim_15"].clip.length);
		isRunning=false;
		
		
	}

	
	IEnumerator extremeAnimation16(){
		
		
		isRunning=true;
		animation["extremeAnim_16"].layer = 5;
		animation["extremeAnim_16"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_16"].speed = 1;
		animation.Play("extremeAnim_16");
		yield return new WaitForSeconds(animation["extremeAnim_16"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation17(){
		
		
		isRunning=true;
		animation["extremeAnim_17"].layer = 5;
		animation["extremeAnim_17"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_17"].speed = 1;
		animation.Play("extremeAnim_17");
		yield return new WaitForSeconds(animation["extremeAnim_17"].clip.length);
		isRunning=false;
		
		
	}
	
	IEnumerator extremeAnimation18(){
		
		
		isRunning=true;
		animation["extremeAnim_18"].layer = 5;
		animation["extremeAnim_18"].wrapMode = WrapMode.ClampForever;
		animation["extremeAnim_18"].speed = 1;
		animation.Play("extremeAnim_18");
		yield return new WaitForSeconds(animation["extremeAnim_18"].clip.length);
		isRunning=false;
		
		
	}
	

	IEnumerator dieAnim(){
		
        trackAnimRun = false;	
		hiz = 0;
		speed = 0;
		animation["dieAnim"].speed =2;
		animation.Play("dieAnim");
		yield return new WaitForSeconds(2f);
		AdSave();
        string pScore = score.ToString();
		      if(Application.systemLanguage == SystemLanguage.Turkish){
		      Instantiate(TotalScoreTR,TotalScoreTR.transform.position,TotalScoreTR.transform.rotation);					
			}
		
				else{			
					Instantiate(TotalScoreEN,TotalScoreEN.transform.position,TotalScoreEN.transform.rotation);
				}
		
					Destroy(gameObject);
					Destroy(GameObject.FindGameObjectWithTag("Pause"));
					Destroy(scoreBx);
		}
    

    // Collider ve Triger kontrolleri
    
	void OnCollisionExit(Collision ext){

		if(ext.gameObject.tag == "RampExt"){
			checkTrackAnim = false;
			animComing();
		}
	}


	void OnTriggerEnter(Collider clc){

		if(clc.transform.tag == "Fish"){
			score+=100;
			scoreBx.animation.Play();
			audioSources[1].Play();
		}
	}

	void OnCollisionEnter(Collision clp){
		if(clp.gameObject.tag == "Respawn"){

			trackAnimation.renderer.enabled =true;
		}

		if(clp.gameObject.tag == "Rampa"){
			//trackAnimation.animation["trackAnim"].time =0;
			trackAnimation.animation.Stop();
			trackAnimation.renderer.enabled =false;
			//trackAnimation.animation["trackAnim"].time =0;

		}
	
		if(clp.gameObject.tag == "Enemy"){
		hiz = 0;
		speed = 0;
		StartCoroutine(dieAnim());
		}
	}
	void OnCollisionStay(Collision col){

		if(col.transform.tag == "Respawn"){
				if(checkTrackAnim == true){

				trackAnimation.animation.Play();
				}

				if(timeBackInt == -1){
				if(audioSources[7].isPlaying == false){
					audioSources[7].audio.loop = true;
					audioSources[7].Play();
			}
		}
            
     // Dokunmatik Kontrolleri
			
			foreach (Touch touch in Input.touches){
				
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Moved){
					if(timeBackInt == -1){
					randomNumber = Random.Range(0,5);
					rigidbody.velocity = new Vector3(0,5,0);
					jumpComing();
					}
				}
			}				
		}
	}
    
    //Score Game UI

	void OnGUI(){

		pScore = score.ToString();
   		GUI.Label(new Rect(Screen.width/ypos-Screen.width, Screen.height/xpos-Screen.height,Screen.width/3,Screen.height/3),pScore,style);
		style.fontSize = (int) (Screen.width * 0.04f);	
	}
}
