using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suiciniv : MonoBehaviour
{
    public int life;
    public enum actions {Walk, Rush, Punch, NoAction, Stun};
    private GameObject player;
    public float rush_Range;
    public actions atual;
    public float time_rush, time_stun, time_punch, time_walk, time_damaged, time_stun_imune;
    private float time_rush_elapsed, time_stun_elapsed, time_punch_elapsed, time_walk_elapsed, time_damaged_elapsed, time_stun_imune_elapsed;
    public float step_length, step_walk, step_length_medium, step_walk_medium, step_length_low, step_length_atual, step_walk_atual, step_walk_low, height_adjust, distance_adjust;
    private Animator anim_control;
    private bool on_range_punch, damaged, stun_imune;

    public PolygonCollider2D collider_walk, collider_rush, collider_atk;
    public PolygonCollider2D collider_walk2, collider_rush2, collider_atk2;

    public GameObject light_esq_left, light_esq_right, light_dir_left, light_dir_right;

    public GameObject wall;

    public Material ilum;
    private bool ilu_active;

    public GameObject end;
    public float adjust_end_x, adjust_end_y;

    public GameObject music;
    public AudioClip end_music, end_music2;

    // Use this for initialization
    void Start ()
    {
        atual = actions.NoAction;
        life = 1000;
        player = GameObject.FindGameObjectWithTag("Player");
        anim_control = GetComponentInChildren<Animator>();
        on_range_punch = false;
        damaged = false;
        time_damaged = 0.1f;
        time_damaged_elapsed = 0;
        stun_imune = false;
        time_stun_imune = 5f;
        ilu_active = false;
        ilum.SetColor("_Color", Color.white);
        music.GetComponent<AudioSource>().Stop();
        music.GetComponent<AudioSource>().clip = end_music;
        music.GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
       
        if (atual == actions.NoAction) search_action();
       
        if (atual == actions.Rush) rush();

        if (atual == actions.Stun) stun();

        if (atual == actions.Punch) punch();

        if (atual == actions.Walk) walk();

        update_times();
        color_damaged();
        collider_adjust();
        difficulty();   
        adjust_euler_angles();
        adjust_lights();
        check_dead();
    }

    void check_dead()
    {
        if (life <= 0)
        {
            Instantiate(end, player.GetComponent<Transform>().position + new Vector3(adjust_end_x, adjust_end_y)
                , player.GetComponent<Transform>().rotation);
            player.GetComponent<Isayah>().speed = 0;
            ilum.SetColor("_Color", Color.white);
            music.GetComponent<AudioSource>().Stop();
            music.GetComponent<AudioSource>().clip = end_music2;
            music.GetComponent<AudioSource>().Play();            
            Destroy(gameObject);
        }
        
    }

    void adjust_lights()
    {
        if (!GetComponentInChildren<SpriteRenderer>().flipX)
        {
            light_esq_left.SetActive(true);
            light_esq_right.SetActive(true);
            light_dir_left.SetActive(false);
            light_dir_right.SetActive(false);

        }
        else
        {
            light_esq_left.SetActive(false);
            light_esq_right.SetActive(false);
            light_dir_left.SetActive(true);
            light_dir_right.SetActive(true);
        }
    }
    
    void adjust_euler_angles()
    {
        if (player.GetComponent<Isayah>().state == false && GetComponent<Transform>().position.z > 0)
        {
            GetComponent<Transform>().Translate(0, 0, -9);
        }
        else if (player.GetComponent<Isayah>().state == true && GetComponent<Transform>().position.z < 9)
        {
            GetComponent<Transform>().Translate(0, 0, 9);
        }
    }

    void difficulty()
    {
        if (life >= 700)        step_length_atual = step_length;
        else if (life >= 300)   step_length_atual = step_length_medium;
        else                    step_length_atual = step_length_low;

        if (life >= 700)        step_walk_atual = step_walk;
        else if (life >= 300)   step_walk_atual = step_walk_medium;
        else                    step_walk_atual = step_walk_low;

        if (life <= 300 && !ilu_active)
        {
            ilu_active = true;
            ilum.SetColor("_Color", Color.red);
        }
    }

    void collider_adjust()
    {
        if (!GetComponentInChildren<SpriteRenderer>().flipX)
        {
            if (atual == actions.NoAction)
            {
                collider_walk.enabled = true;
                collider_rush.enabled = false;
                collider_atk.enabled = false;
                collider_walk2.enabled = false;
                collider_rush2.enabled = false;
                collider_atk2.enabled = false;
            }
            else if (atual == actions.Rush)
            {
                collider_walk.enabled = false;
                collider_rush.enabled = true;
                collider_atk.enabled = false;
                collider_walk2.enabled = false;
                collider_rush2.enabled = false;
                collider_atk2.enabled = false;
            }
            else if (atual == actions.Stun)
            {
                collider_walk.enabled = false;
                collider_rush.enabled = true;
                collider_atk.enabled = false;
                collider_walk2.enabled = false;
                collider_rush2.enabled = false;
                collider_atk2.enabled = false;
            }
            else if (atual == actions.Punch)
            {
                collider_walk.enabled = false;
                collider_rush.enabled = false;
                collider_atk.enabled = true;
                collider_walk2.enabled = false;
                collider_rush2.enabled = false;
                collider_atk2.enabled = false;
            }
            else if (atual == actions.Walk)
            {
                collider_walk.enabled = true;
                collider_rush.enabled = false;
                collider_atk.enabled = false;
                collider_walk2.enabled = false;
                collider_rush2.enabled = false;
                collider_atk2.enabled = false;
            }
        }
        else
        {
            if (atual == actions.NoAction)
            {
                collider_walk.enabled = false;
                collider_rush.enabled = false;
                collider_atk.enabled = false;
                collider_walk2.enabled = true;
                collider_rush2.enabled = false;
                collider_atk2.enabled = false;
            }
            else if (atual == actions.Rush)
            {
                collider_walk.enabled = false;
                collider_rush.enabled = false;
                collider_atk.enabled = false;
                collider_walk2.enabled = false;
                collider_rush2.enabled = true;
                collider_atk2.enabled = false;
            }
            else if (atual == actions.Stun)
            {
                collider_walk.enabled = false;
                collider_rush.enabled = false;
                collider_atk.enabled = false;
                collider_walk2.enabled = false;
                collider_rush2.enabled = true;
                collider_atk2.enabled = false;
            }
            else if (atual == actions.Punch)
            {
                collider_walk.enabled = false;
                collider_rush.enabled = false;
                collider_atk.enabled = false;
                collider_walk2.enabled = false;
                collider_rush2.enabled = false;
                collider_atk2.enabled = true;
            }
            else if (atual == actions.Walk)
            {
                collider_walk.enabled = false;
                collider_rush.enabled = false;
                collider_atk.enabled = false;
                collider_walk2.enabled = true;
                collider_rush2.enabled = false;
                collider_atk2.enabled = false;
            }
        }
        
    }

    void color_damaged()
    {
        if (this.damaged) GetComponentInChildren<SpriteRenderer>().color = Color.red;        
        else  GetComponentInChildren<SpriteRenderer>().color = Color.white;
               
        if ((this.time_damaged_elapsed >= this.time_damaged) && this.damaged)
        {
            damaged = false;
            this.time_damaged_elapsed = 0;
        }
    }

    void walk()
    {
        if (!on_range_punch)
        {
            if (!GetComponentInChildren<SpriteRenderer>().flipX) transform.Translate(Vector2.left * step_walk_atual * Time.deltaTime);
            else transform.Translate(Vector2.right * step_walk_atual * Time.deltaTime);
        }
        else
        {
            atual = actions.NoAction;
            anim_control.SetTrigger("Stop_walk");
        }
    }

    void punch()
    {
        if (time_punch_elapsed >= time_punch)
        {
            atual = actions.NoAction;
            if (player_position())
            {
                Instantiate(wall, new Vector3(  player.GetComponent<Transform>().position.x - distance_adjust, 
                                                player.GetComponent<Transform>().position.y + height_adjust, 
                                                player.GetComponent<Transform>().position.z), 
                                 player.GetComponent<Transform>().rotation);
            }
            else
            {
                Instantiate(wall, new Vector3(player.GetComponent<Transform>().position.x + distance_adjust,
                                                player.GetComponent<Transform>().position.y + height_adjust,
                                                player.GetComponent<Transform>().position.z),
                                 player.GetComponent<Transform>().rotation);
            }
            
        }
    }

    void stun()
    {
        print("stuned");
        if (time_stun_elapsed >= time_stun)
        {
            atual = actions.NoAction;
            anim_control.SetTrigger("Stop_stun");
        }
    }

    void rush()
    {
        if (!GetComponentInChildren<SpriteRenderer>().flipX) transform.Translate(Vector2.left * step_length_atual * Time.deltaTime);
        else transform.Translate(Vector2.right * step_length_atual * Time.deltaTime);
    }

    void update_times()
    {
        time_rush_elapsed += Time.deltaTime;
        time_stun_elapsed += Time.deltaTime;
        time_punch_elapsed += Time.deltaTime;
        time_walk_elapsed += Time.deltaTime;
        time_damaged_elapsed += Time.deltaTime;
        time_stun_imune_elapsed += Time.deltaTime;
        if(time_stun_imune_elapsed >= time_stun_imune)
        {
            time_stun_imune_elapsed = 0;
            stun_imune = false;
        }
    }

    void search_action()
    {
    
        if (player_position()) GetComponentInChildren<SpriteRenderer>().flipX = false;
        else GetComponentInChildren<SpriteRenderer>().flipX = true;
            
        if (time_rush_elapsed >= time_rush)
        {
            atual = actions.Rush;
            time_rush_elapsed = 0;
            anim_control.SetTrigger("Rush");

        }else if(on_range_punch)
        {
            atual = actions.Punch;
            time_punch_elapsed = 0;
            anim_control.SetTrigger("Atk");
        }
        else
        {
            atual = actions.Walk;
            time_walk_elapsed = 0;
            anim_control.SetTrigger("Walk");
        }
        
    }

    /// <summary>
    /// calculates whether the player is to the right or left of the boss
    /// </summary>
    /// <returns> True, if the player is on left
    ///           False, if the player is on right </returns>
    bool player_position()
    {
        if (player.transform.position.x - transform.position.x <= 0) return true;
        else return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "wall") && !(stun_imune) && (atual == actions.Rush))
        {
            print("hit wall");
            atual = actions.Stun;
            time_stun_elapsed = 0;
            anim_control.SetTrigger("Stun");
            stun_imune = true;
            time_stun_imune_elapsed = 0;
        }
        if (collision.gameObject.tag == "Player")
        {
            print("player hited");
        }
        if (collision.gameObject.tag == "fireball")
        {
            life -= 50;
            damaged = true;
            time_damaged_elapsed = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            on_range_punch = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            on_range_punch = false;
        }
    }
}
