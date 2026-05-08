using System.Collections.Generic;
using UnityEngine;

public class GatheringGenerator : MonoBehaviour
{
    // Prefabs para generar los objetos
    [SerializeField] GameObject _fibberPrefab; // Prefab del objeto "fibber"
    [SerializeField] GameObject _treePrefab; // Prefab del objeto "tree"
    [SerializeField] GameObject _stonePrefab; // Prefab del objeto "stone"
    [SerializeField] GameObject _copperPrefab; // Prefab del objeto "copper"
    [SerializeField] GameObject _ironPrefab; // Prefab del objeto "iron"
    [SerializeField] GameObject _goldPrefab; // Prefab del objeto "gold"
    [SerializeField] GameObject _meleEnemyPrefab; // Prefab del enemigo de melee
    [SerializeField] GameObject _rangedEnemyPrefab; // Prefab del enemigo a distancia

    // Listas de objetos generados
    public List<GameObject> fibberOreList; // Lista de objetos "fibber" generados
    [SerializeField] List<GameObject> _treeOreList; // Lista de objetos "tree" generados
    [SerializeField] List<GameObject> _stoneOreList; // Lista de objetos "stone" generados
    [SerializeField] List<GameObject> _copperOreList; // Lista de objetos "copper" generados
    [SerializeField] List<GameObject> _ironOreList; // Lista de objetos "iron" generados
    [SerializeField] List<GameObject> _goldOreList; // Lista de objetos "gold" generados
    [SerializeField] List<GameObject> _meleEnemiyList; // Lista de enemigos de melee generados
    [SerializeField] List<GameObject> _rangedEnemiyList; // Lista de enemigos a distancia generados

    [SerializeField] List<PolygonCollider2D> _waterCollidersLists; // Lista de colliders de agua

    // Variables de cantidad máxima y tiempo de generación
    public int maxFibber, maxTree, maxStone, maxCopper, maxIron, maxGold, maxMeleEnemy, maxRangedEnemy; // Cantidades máximas permitidas
    public float fibberTime, treeTime, stoneTime, copperTime, ironTime, goldTime, meleTime, rangedTime, generalTimer; // Tiempos de generación

    [SerializeField] GameObject _deleters; // GameObject que desactiva los objetos "_deleters"

    void Start()
    {
        // Configuración de las cantidades máximas y tiempos de generación iniciales
        maxFibber = 60;
        maxTree = 60;
        maxStone = 60;
        maxCopper = 45;
        maxIron = 30;
        maxGold = 15;
        maxMeleEnemy = 45;
        maxRangedEnemy = 30;

        // Inicialización de los tiempos de generación
        fibberTime = 0f + Time.deltaTime;
        treeTime = 0f + Time.deltaTime;
        stoneTime = 0f + Time.deltaTime;
        copperTime = 0f + Time.deltaTime;
        ironTime = 0f + Time.deltaTime;
        goldTime = 0f + Time.deltaTime;
        meleTime = 0f + Time.deltaTime;
        rangedTime = 0f + Time.deltaTime;
    }

    // Desactiva los objetos "_deleters"
    public void DisableDeleters()
    {
        _deleters.SetActive(false);
    }

    void Update()
    {
        fibberTime += Time.deltaTime;
        treeTime += Time.deltaTime;
        stoneTime += Time.deltaTime;
        copperTime += Time.deltaTime;
        ironTime += Time.deltaTime;
        goldTime += Time.deltaTime;
        meleTime += Time.deltaTime;
        rangedTime += Time.deltaTime;


        if (generalTimer < 5)
        {
            // Generación de objetos "fibber"
            if (fibberOreList.Count < maxFibber && fibberTime >= 0.1f)
            {
                _deleters.SetActive(true);
                fibberOreList.Add(Instantiate(_fibberPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _fibberPrefab.transform.rotation));
                fibberTime = 0;
                Invoke(nameof(DisableDeleters), 1f);

            }
            else if (fibberOreList.Count == maxFibber)
            {
                fibberTime = 0;
                CheckAllLists(fibberOreList);
            }

            // Generación de objetos "tree"
            if (_treeOreList.Count < maxTree && treeTime >= 0.1f)
            {
                _deleters.SetActive(true);
                _treeOreList.Add(Instantiate(_treePrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _treePrefab.transform.rotation));
                treeTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_treeOreList.Count == maxTree)
            {
                treeTime = 0;
                CheckAllLists(_treeOreList);
            }

            // Generación de objetos "stone"
            if (_stoneOreList.Count < maxStone && stoneTime >= 0.1f)
            {
                _deleters.SetActive(true);
                _stoneOreList.Add(Instantiate(_stonePrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _stonePrefab.transform.rotation));
                stoneTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_stoneOreList.Count == maxStone)
            {
                stoneTime = 0;
                CheckAllLists(_stoneOreList);
            }

            // Generación de objetos "copper"
            if (_copperOreList.Count < maxCopper && copperTime >= 0.1f)
            {
                _deleters.SetActive(true);
                _copperOreList.Add(Instantiate(_copperPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _copperPrefab.transform.rotation));
                copperTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_copperOreList.Count == maxCopper)
            {
                copperTime = 0;
                CheckAllLists(_copperOreList);
            }

            // Generación de objetos "iron"
            if (_ironOreList.Count < maxIron && ironTime >= 0.1f)
            {
                _deleters.SetActive(true);
                _ironOreList.Add(Instantiate(_ironPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _ironPrefab.transform.rotation));
                ironTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_ironOreList.Count == maxIron)
            {
                ironTime = 0;
                CheckAllLists(_ironOreList);
            }

            // Generación de objetos "gold"
            if (_goldOreList.Count < maxGold && goldTime >= 0.1f)
            {
                _deleters.SetActive(true);
                _goldOreList.Add(Instantiate(_goldPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _goldPrefab.transform.rotation));
                goldTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_goldOreList.Count == maxGold)
            {
                goldTime = 0;
                CheckAllLists(_goldOreList);
            }

            // Generación de objetos "mele enemy"
            if (_meleEnemiyList.Count < maxMeleEnemy && meleTime >= 0.1f)
            {
                _deleters.SetActive(true);
                _meleEnemiyList.Add(Instantiate(_meleEnemyPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _meleEnemyPrefab.transform.rotation));
                meleTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_meleEnemiyList.Count == maxMeleEnemy)
            {
                meleTime = 0;
                CheckAllLists(_meleEnemiyList);
            }

            // Generación de objetos "ranged enemy"
            if (_rangedEnemiyList.Count < maxRangedEnemy && rangedTime >= 0.1f)
            {
                _deleters.SetActive(true);
                _rangedEnemiyList.Add(Instantiate(_rangedEnemyPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _rangedEnemyPrefab.transform.rotation));
                rangedTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_rangedEnemiyList.Count == maxRangedEnemy)
            {
                rangedTime = 0;
                CheckAllLists(_rangedEnemiyList);
            }
        }
        else
        {
            if (fibberOreList.Count < maxFibber && fibberTime >= 1f)
            {
                _deleters.SetActive(true);
                fibberOreList.Add(Instantiate(_fibberPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _fibberPrefab.transform.rotation));
                fibberTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (fibberOreList.Count == maxFibber)
            {
                fibberTime = 0;
                CheckAllLists(fibberOreList);
            }

            if (_treeOreList.Count < maxTree && treeTime >= 1f)
            {
                _deleters.SetActive(true);
                _treeOreList.Add(Instantiate(_treePrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _treePrefab.transform.rotation));
                treeTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_treeOreList.Count == maxTree)
            {
                treeTime = 0;
                CheckAllLists(_treeOreList);
            }

            if (_stoneOreList.Count < maxStone && stoneTime >= 1f)
            {
                _deleters.SetActive(true);
                _stoneOreList.Add(Instantiate(_stonePrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _stonePrefab.transform.rotation));
                stoneTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_stoneOreList.Count == maxStone)
            {
                stoneTime = 0;
                CheckAllLists(_stoneOreList);
            }

            if (_copperOreList.Count < maxCopper && copperTime >= 1f)
            {
                _deleters.SetActive(true);
                _copperOreList.Add(Instantiate(_copperPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _copperPrefab.transform.rotation));
                copperTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_copperOreList.Count == maxCopper)
            {
                copperTime = 0;
                CheckAllLists(_copperOreList);
            }

            if (_ironOreList.Count < maxIron && ironTime >= 1f)
            {
                _deleters.SetActive(true);
                _ironOreList.Add(Instantiate(_ironPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _ironPrefab.transform.rotation));
                ironTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_ironOreList.Count == maxIron)
            {
                ironTime = 0;
                CheckAllLists(_ironOreList);
            }

            if (_goldOreList.Count < maxGold && goldTime >= 0.5f)
            {
                _deleters.SetActive(true);
                _goldOreList.Add(Instantiate(_goldPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _goldPrefab.transform.rotation));
                goldTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_goldOreList.Count == maxGold)
            {
                goldTime = 0;
                CheckAllLists(_goldOreList);
            }

            if (_meleEnemiyList.Count < maxMeleEnemy && meleTime >= 1f)
            {
                _deleters.SetActive(true);
                _meleEnemiyList.Add(Instantiate(_meleEnemyPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _meleEnemyPrefab.transform.rotation));
                meleTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_meleEnemiyList.Count == maxMeleEnemy)
            {
                meleTime = 0;
                CheckAllLists(_meleEnemiyList);
            }

            if (_rangedEnemiyList.Count < maxRangedEnemy && rangedTime >= 1f)
            {
                _deleters.SetActive(true);
                _rangedEnemiyList.Add(Instantiate(_rangedEnemyPrefab, new Vector3(Random.Range(-209, 207.5f), Random.Range(164, -66), 0), _rangedEnemyPrefab.transform.rotation));
                rangedTime = 0;
                Invoke(nameof(DisableDeleters), 1f);
            }
            else if (_rangedEnemiyList.Count == maxRangedEnemy)
            {
                rangedTime = 0;
                CheckAllLists(_rangedEnemiyList);
            }
        }

    }

    private void CheckAllLists(List<GameObject> list)
    {

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                list.Remove(list[i]);
            }
        }

    }
}