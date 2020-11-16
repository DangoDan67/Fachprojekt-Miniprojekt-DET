using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class waveSpawner : MonoBehaviour
{
    
    public enum SpawnState {SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class wave
    {
        public string name;
        public GameObject enemy1;
        public GameObject enemy2;
        public int count;
        public float rate;
    }
    public wave[] waves;
    private int nextWave = 0;

    public GameObject score;
    public GameObject itemsContainer;

    public Transform[] spawnPoints;

    public float timeBetweenWaves;
    private float waveCountdown;

    private SpawnState state = SpawnState.COUNTING;

    private float searchCountdown = 1f;

    public float addHealth = 0;
    public float addHitForce = 0;
    public int addEnemy = 0;

    private int scoreToChange = 30;

    void Start() {
        if(spawnPoints.Length == 0){
            Debug.Log("No SpawnPoints referenced! ");
        }
         waveCountdown = timeBetweenWaves;    
    }

    void Update() {
        if(state == SpawnState.WAITING){
            if(!EnemyIsAlive()){
                waveCompleted();
            }else{
                return;
            }
        }

        if (waveCountdown <= 0){
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }else{
            waveCountdown -= Time.deltaTime;
        }    
    }

    void waveCompleted(){
        if (score.GetComponent<score>().getIntScore() >= scoreToChange) {
            this.addHealth += 10f;
            this.addHitForce += 3f;
            this.scoreToChange += 10;
            this.addEnemy += 1;
        }
        Debug.Log("wave Completed!");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if(nextWave + 1 > waves.Length -1 ){
            nextWave = 0;
            Debug.Log("All Waves Complete!");
        }else{
            nextWave++;
        }
        
    }

    bool EnemyIsAlive(){
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f){
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null){
                return false;
            }
            else
            {
                Debug.Log("ES gibt noch enemys");
            }
        }
        return true;
    }

    IEnumerator SpawnWave (wave _wave){
        Debug.Log("Spawning Wave: "+ _wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0 ; i< (_wave.count + addEnemy) ; i++){
            StartCoroutine(SpawnEnemy(_wave.enemy1)) ;
            yield return new WaitForSeconds(1);
            StartCoroutine( SpawnEnemy(_wave.enemy2));
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    IEnumerator SpawnEnemy(GameObject _enemy){
        //Hier müsste glaube ich ein kleiner ZeitBuffer hin
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
        GameObject enemy1 = Instantiate(_enemy, _sp.position, _sp.rotation);
        enemy1.GetComponent<EnemyBehavior>().initialValue(score,itemsContainer, addHealth, addHitForce);
        yield return new WaitForSeconds(5);
        GameObject enemy2 = Instantiate(_enemy, _sp.position, _sp.rotation);
        enemy2.GetComponent<EnemyBehavior>().initialValue(score,itemsContainer, addHealth, addHitForce);
    }
}
