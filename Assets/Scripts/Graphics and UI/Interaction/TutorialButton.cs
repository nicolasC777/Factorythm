using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TutorialButton : Machine {
    [SerializeField] private GameObject door;
    private GameObject _myGameObject;

    private Vector3 _spawnPoint;

    private SpriteRenderer _icon;

    private Vector3 _iconUp;
    public Vector3 IconDown;

    public Sprite DeInteract;
    public Sprite Interact;
    [SerializeField] private SpriteRenderer _mySR;

    public GameObject LockedRoomObj;
    private LockedRoom _lockedRoom;

    private void Start() {
        _spawnPoint = transform.Find("SpawnPoint").position;
        _icon = transform.Find("Icon").GetComponent<SpriteRenderer>();
        base.Start();
        _iconUp = _icon.transform.localPosition;
        _lockedRoom = LockedRoomObj.GetComponent<LockedRoom>();
    }

    public override void OnInteract(PlayerController p) {
        if (_myGameObject == null) {
            _myGameObject = Instantiate(door, _spawnPoint, Quaternion.identity);
        }
        base.OnInteract(p);
        _icon.transform.localPosition = IconDown;
        _mySR.sprite = Interact;
    }

    public override void OnDeInteract(PlayerController p) {
        base.OnDeInteract(p);
        _icon.transform.localPosition = _iconUp;
        _mySR.sprite = DeInteract;

        TutorialDoor d = p.OnComponent<TutorialDoor>(p.transform.position);
        if (d != null) {
            _lockedRoom.gameObject.SetActive(false);
        }
    }

    public void Unlock() {
        gameObject.SetActive(true);
    }
}
