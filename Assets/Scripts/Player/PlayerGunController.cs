using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGunController : MonoBehaviour
{
    //Initialize variables
    GameObject player;
    GameObject cursor;
    SpriteRenderer rend;
    float cursorx;
    float cursory;
    float angle;
    float accBoundSMG;
    float accBoundSpingun;
    public float spinDelay;
    Vector3 disp;
    public Vector2 bulletSpeed;
    public Vector2 rocketSpeed;
    AudioSource source;
    public int maxAmmoPistol = 8;
    public int maxAmmoShotgun = 6;
    public int maxAmmoSmg = 30;
    public int maxAmmoZooka = 3;
    public int maxAmmoRevolver = 5;
    public int maxAmmoSpingun = 45;
    public int currentAmmo;
    public bool initialize = true;
    bool canShoot = true;
    int canShootTimer = 0;
    bool reload = false;
    int reloadTimer = 0;
    GameObject pistolCounter;
    GameObject shotgunCounter;
    GameObject smgCounter;
    GameObject zookaCounter;
    GameObject revolverCounter;
    GameObject spingunCounter;
    PlayerController playerScript;
    CameraController camScript;
    Sprite pistolSprite;
    Sprite shotgunSprite;
    Sprite smgSprite;
    Sprite zookaSprite;
    Sprite revolverSprite;
    Sprite spingunSprite;
    AudioClip pistolSound;
    AudioClip shotgunSound;
    AudioClip smgSound;
    AudioClip zookaSound;
    AudioClip revolverSound;
    AudioClip spingunSound;
    GameObject pistolBullet;
    GameObject shotgunBullet;
    GameObject smgBullet;
    GameObject zookaBullet;
    GameObject revolverBullet;
    PlatformInput inputHandler;
    GameObject shell;
    Sprite pistolShell;
    Sprite shotgunShell;
    Sprite smgShell;
    Sprite revolverShell;
    ParticleSystem partS;
    bool canDrop = false;

    public enum Gun
    {
        pistol,
        shotgun,
        smg,
        zooka,
        revolver,
        spingun
    }

    public Gun currentGun = Gun.pistol;

    // Use this for initialization
    void Start()
    {
        //Gun displacement
        disp = new Vector3(0.4f, 0, 0);
        //Bullet variables
        bulletSpeed = new Vector2(12, 0);
        rocketSpeed = new Vector2(16, 0);
        accBoundSMG = 10f;
        accBoundSpingun = 1f;
        spinDelay = 10f;
        //Find relevant objects
        player = transform.parent.Find("Player(Clone)").gameObject;
        cursor = transform.parent.Find("Crosshair(Clone)").gameObject;
        pistolCounter = GameObject.Find("Pistol Ammo");
        shotgunCounter = GameObject.Find("Shotgun Ammo");
        smgCounter = GameObject.Find("SMG Ammo");
        zookaCounter = GameObject.Find("Boomzooka Ammo");
        revolverCounter = GameObject.Find("Revolver Ammo");
        spingunCounter = GameObject.Find("Spingun Ammo");
        //Get & create components
        rend = GetComponent<SpriteRenderer>();
        source = gameObject.AddComponent<AudioSource>();
        playerScript = player.GetComponent<PlayerController>();
        partS = GetComponent<ParticleSystem>();
        //Find camera
        camScript = GameObject.Find("Main Camera").GetComponent<CameraController>();
        //Load resources
        pistolSprite = Resources.Load<Sprite>(@"Sprites\Pistol");
        shotgunSprite = Resources.Load<Sprite>(@"Sprites\Shotgun");
        smgSprite = Resources.Load<Sprite>(@"Sprites\SMG");
        zookaSprite = Resources.Load<Sprite>(@"Sprites\Boomzooka");
        revolverSprite = Resources.Load<Sprite>(@"Sprites\Revolver");
        spingunSprite = Resources.Load<Sprite>(@"Sprites\Spingun");
        pistolBullet = (GameObject)Resources.Load(@"Bullets\Pistol Bullet");
        shotgunBullet = (GameObject)Resources.Load(@"Bullets\Shotgun Bullet");
        smgBullet = (GameObject)Resources.Load(@"Bullets\SMG Bullet");
        zookaBullet = (GameObject)Resources.Load(@"Bullets\Rocket");
        revolverBullet = (GameObject)Resources.Load(@"Bullets\Revolver Bullet");
        pistolSound = (AudioClip)Resources.Load("Sounds/gun1");
        shotgunSound = (AudioClip)Resources.Load("Sounds/shotgun1");
        smgSound = (AudioClip)Resources.Load("Sounds/smg1");
        zookaSound = (AudioClip)Resources.Load("Sounds/zooka1");
        revolverSound = (AudioClip)Resources.Load("Sounds/revolver1");
        spingunSound = (AudioClip)Resources.Load("Sounds/spingun1");
        shell = (GameObject)Resources.Load("Shell");
        pistolShell = Resources.Load<Sprite>(@"Sprites\Pistol Shell");
        shotgunShell = Resources.Load<Sprite>(@"Sprites\Shotgun Shell");
        smgShell = Resources.Load<Sprite>(@"Sprites\SMG Shell");
        revolverShell = Resources.Load<Sprite>(@"Sprites\Revolver Shell");

        //Get input handler
        inputHandler = GameObject.Find("Input Handler").GetComponent<PlatformInput>();
    }

    //Counters and stuff
    void FixedUpdate()
    {
        //Refire reducer and cleaner
        canShootTimer--;
        if (canShootTimer <= 0)
        {
            canShoot = true;
            canShootTimer = 0;
        }

        //Refire reducer and cleaner
        reloadTimer--;
        if (reloadTimer <= 0 && reload && currentGun == Gun.pistol)
        {
            reload = false;
            reloadTimer = 0;
            initialize = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Initialize weapons
        if (initialize == true)
        {
            switch (currentGun)
            {
                case Gun.pistol:
                    {
                        currentAmmo = maxAmmoPistol;
                        rend.sprite = pistolSprite;
                        DisableAmmo();
                        pistolCounter.SetActive(true);
                    }
                    break;

                case Gun.shotgun:
                    {
                        currentAmmo = maxAmmoShotgun;
                        rend.sprite = shotgunSprite;
                        DisableAmmo();
                        shotgunCounter.SetActive(true);
                    }
                    break;

                case Gun.smg:
                    {
                        currentAmmo = maxAmmoSmg;
                        rend.sprite = smgSprite;
                        DisableAmmo();
                        smgCounter.SetActive(true);
                    }
                    break;

                case Gun.zooka:
                    {
                        currentAmmo = maxAmmoZooka;
                        rend.sprite = zookaSprite;
                        DisableAmmo();
                        zookaCounter.SetActive(true);
                    }
                    break;

                case Gun.revolver:
                    {
                        currentAmmo = maxAmmoRevolver;
                        rend.sprite = revolverSprite;
                        DisableAmmo();
                        revolverCounter.SetActive(true);
                    }
                    break;

                case Gun.spingun:
                    {
                        currentAmmo = maxAmmoSpingun;
                        rend.sprite = spingunSprite;
                        spinDelay = 10f;
                        DisableAmmo();
                        spingunCounter.SetActive(true);
                    }
                    break;
            }
            initialize = false;
            canShoot = true;
        }

        //Rotation and position
        cursorx = cursor.transform.position.x - player.transform.position.x;
        cursory = cursor.transform.position.y - player.transform.position.y;
        angle = Mathf.Atan2(cursory, cursorx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //Flip sprite
        if ((transform.eulerAngles.z > 90) && (transform.eulerAngles.z < 270))
        {
            rend.flipY = true;
        }
        else rend.flipY = false;

        //Assign position
        transform.position = player.transform.position + transform.rotation * disp;

        //Shooting
        switch (currentGun)
        {
            case Gun.pistol:
                {
                    if ((inputHandler.input == PlatformInput.InputState.beginShoot) && canShoot && !reload)
                    {
                        //Accuracy
                        //float acc = Random.Range(-accBound, accBound);
                        //Instantiate bullet
                        GameObject bulletInstance = Instantiate(pistolBullet);
                        bulletInstance.transform.position = transform.position;
                        bulletInstance.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 0));
                        //Set bullet velocity
                        Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
                        bulletrb2d.velocity = bulletInstance.transform.rotation * bulletSpeed;

                        //Sound
                        source.PlayOneShot(pistolSound);

                        //Camera shake
                        camScript.intensity = 0.1f;

                        //Disable refire
                        canShoot = false;
                        canShootTimer = 10;

                        //Drop a shell
                        GameObject shellInstance = Instantiate(shell);
                        shellInstance.transform.position = transform.position;
                        shellInstance.GetComponent<SpriteRenderer>().sprite = pistolShell;

                        //Reduce ammo, reinitialize pistol
                        currentAmmo--;
                        if (currentAmmo == 0)
                        {
                            reload = true;
                            reloadTimer = 80;
                        }
                    }
                }
            break;

            case Gun.shotgun:
                {
                    if ((inputHandler.input == PlatformInput.InputState.beginShoot) && canShoot)
                    {
                        //Accuracy
                        //float acc = Random.Range(-accBound, accBound);
                        //Instantiate bullets
                        for (int i = 0; i < 4; i++)
                        {
                            GameObject bulletInstance = Instantiate(shotgunBullet);
                            bulletInstance.transform.position = transform.position;
                            bulletInstance.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, (i * 10) - 15));
                            //Set bullet velocity
                            Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
                            bulletrb2d.velocity = bulletInstance.transform.rotation * bulletSpeed;
                        }

                        //Sound
                        source.PlayOneShot(shotgunSound);

                        //Recoil
                        /*playerScript.recoiling = true;
                        Vector2 gunRecoil = transform.rotation * Vector2.right * 2;
                        playerScript.recoil -= gunRecoil;*/

                        //Camera shake
                        camScript.intensity = 0.3f;

                        //Disable refire
                        canShoot = false;
                        canShootTimer = 20;

                        //Allow a shell to be dropped
                        canDrop = true;

                        //Reduce ammo, reinitialize pistol
                        currentAmmo--;
                        if (currentAmmo == 0)
                        {
                            currentGun = Gun.pistol;
                            initialize = true;
                            canShoot = false;
                        }
                    }
                    //Debug.Log(canShootTimer);
                    //Check if about to be able to shoot again
                    if (canShootTimer == 1 && canDrop == true)
                    {
                        //Drop a shell
                        canDrop = false;
                        GameObject shellInstance = Instantiate(shell);
                        shellInstance.transform.position = transform.position;
                        shellInstance.GetComponent<SpriteRenderer>().sprite = shotgunShell;
                    }
                }
            break;

            case Gun.smg:
                {
                    if (((inputHandler.input == PlatformInput.InputState.beginShoot) || (inputHandler.input == PlatformInput.InputState.shooting)) && canShoot)
                    {
                        //Accuracy
                        float acc = Random.Range(-accBoundSMG, accBoundSMG);
                        //Instantiate bullet
                        GameObject bulletInstance = Instantiate(smgBullet);
                        bulletInstance.transform.position = transform.position;
                        bulletInstance.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, acc));
                        //Set bullet velocity
                        Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
                        bulletrb2d.velocity = bulletInstance.transform.rotation * bulletSpeed;

                        //Sound
                        source.PlayOneShot(smgSound);

                        //Camera shake
                        camScript.intensity = 0.14f;

                        //Disable refire
                        canShoot = false;
                        canShootTimer = 3;

                        //Drop a shell
                        GameObject shellInstance = Instantiate(shell);
                        shellInstance.transform.position = transform.position;
                        shellInstance.GetComponent<SpriteRenderer>().sprite = smgShell;

                        //Reduce ammo, reinitialize pistol
                        currentAmmo--;
                        if (currentAmmo == 0)
                        {
                            currentGun = Gun.pistol;
                            initialize = true;
                            canShoot = false;
                        }
                    }
                }
            break;

            case Gun.zooka:
                {
                    if ((inputHandler.input == PlatformInput.InputState.beginShoot) && canShoot)
                    {
                        //Create rocket
                        GameObject bulletInstance = Instantiate(zookaBullet);
                        bulletInstance.transform.position = transform.position;
                        bulletInstance.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 0));
                        //Set rocket velocity
                        Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
                        bulletrb2d.velocity = bulletInstance.transform.rotation * rocketSpeed;

                        //Sound
                        source.PlayOneShot(zookaSound);

                        //Camera shake
                        camScript.intensity = 0.8f;

                        //Particles
                        partS.Play();

                        //Disable refire
                        canShoot = false;
                        canShootTimer = 6;

                        //Reduce ammo, reinitialize pistol
                        currentAmmo--;
                        if (currentAmmo == 0)
                        {
                            currentGun = Gun.pistol;
                            initialize = true;
                            canShoot = false;
                        }
                    }
                }
            break;

            case Gun.revolver:
                {
                    if ((inputHandler.input == PlatformInput.InputState.beginShoot) && canShoot)
                    {
                        //Accuracy
                        //float acc = Random.Range(-accBoundSMG, accBoundSMG);
                        //Instantiate bullet
                        GameObject bulletInstance = Instantiate(revolverBullet);
                        bulletInstance.transform.position = transform.position;
                        bulletInstance.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 0));
                        //Set bullet velocity
                        Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
                        bulletrb2d.velocity = bulletInstance.transform.rotation * bulletSpeed;

                        //Sound
                        source.PlayOneShot(revolverSound);

                        //Camera shake
                        camScript.intensity = 0.2f;

                        //Disable refire
                        canShoot = false;
                        canShootTimer = 11;

                        //Drop a shell
                        GameObject shellInstance = Instantiate(shell);
                        shellInstance.transform.position = transform.position;
                        shellInstance.GetComponent<SpriteRenderer>().sprite = revolverShell;

                        //Reduce ammo, reinitialize pistol
                        currentAmmo--;
                        if (currentAmmo == 0)
                        {
                            currentGun = Gun.pistol;
                            initialize = true;
                            canShoot = false;
                        }
                    }
                }
            break;

            case Gun.spingun:
                {
                    if (((inputHandler.input == PlatformInput.InputState.beginShoot) || (inputHandler.input == PlatformInput.InputState.shooting)) && canShoot)
                    {
                        //Decrease spin
                        spinDelay *= 0.9f;
                        //Accuracy
                        accBoundSpingun = 50f / spinDelay;
                        float acc = Random.Range(-accBoundSpingun, accBoundSpingun);
                        //Instantiate bullet
                        GameObject bulletInstance = Instantiate(smgBullet);
                        bulletInstance.transform.position = transform.position;
                        bulletInstance.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, acc));
                        //Set bullet velocity
                        Rigidbody2D bulletrb2d = bulletInstance.GetComponent<Rigidbody2D>();
                        bulletrb2d.velocity = bulletInstance.transform.rotation * bulletSpeed;

                        //Sound
                        source.PlayOneShot(spingunSound);

                        //Camera shake
                        camScript.intensity = 0.1f;

                        //Disable refire
                        canShoot = false;
                        canShootTimer = (int)spinDelay;

                        //Drop a shell
                        GameObject shellInstance = Instantiate(shell);
                        shellInstance.transform.position = transform.position;
                        shellInstance.GetComponent<SpriteRenderer>().sprite = smgShell;

                        //Reduce ammo, reinitialize pistol
                        currentAmmo--;
                        if (currentAmmo == 0)
                        {
                            currentGun = Gun.pistol;
                            initialize = true;
                            canShoot = false;
                        }
                    }
                    else if (inputHandler.input == PlatformInput.InputState.none)
                    {
                        spinDelay *= 1.01f;
                    }
                    spinDelay = Mathf.Clamp(spinDelay, 2, 10);
                }
            break;
        }
    }

    void DisableAmmo()
    {
        pistolCounter.SetActive(false);
        shotgunCounter.SetActive(false);
        smgCounter.SetActive(false);
        zookaCounter.SetActive(false);
        revolverCounter.SetActive(false);
        spingunCounter.SetActive(false);
    }
}
