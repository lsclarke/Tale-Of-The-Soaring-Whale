using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

//Lenard (Nova) Updated: 7/12/2025
//Editor Name/Discord Username, Date
[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class PlatformerPlayer : ScriptableObject
{
    /// <summary>
    /// This class is responsible for all the player stats and characteristics that will be present throught out the game. 
    /// We can use this to call on different states to change and manipulate under difference conditions. There is also a contructor for all the states placed on to the player so that we can create a new player
    /// (Lenard)
    /// </summary>

    public float Health { get; set; }

    public float Stamina { get; set; }

    public float MoveSpeed { get; set; }

    public float JumpSpeed { get; set; }

    public float BounceForce { get; set; }

    public float StompForce { get; set; }

    public float AirThrustForce { get; set; }

    public float AttackPower { get; set; }

    public PlatformerPlayer(float health, float stamina, float speed, float jump_speed, float bounce_force, float stomp_force, float air_thrust_force, float attack)
    {
        this.Health = health;
        this.Stamina = stamina;
        this.MoveSpeed = speed;
        this.JumpSpeed = jump_speed;
        this.BounceForce = bounce_force;
        this.StompForce = stomp_force;
        this.AirThrustForce = air_thrust_force;
        this.AttackPower = attack;
    }

}
