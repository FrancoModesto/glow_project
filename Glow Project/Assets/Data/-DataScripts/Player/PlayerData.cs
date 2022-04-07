using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    //DESIGN PM
    [Header("PLAYER CONFIGS")]
    [SerializeField] private float pSpeed = 0f;
    [SerializeField] private float jumpPower = 0f;
    [SerializeField] private float maxJumpForce = 0f;
    [SerializeField] private float shiftPower = 0f;
    [SerializeField] private float shiftCooldown = 0f;
    [SerializeField] private float fallPower = 0f;
    [SerializeField] private float fallCooldown = 0f;

    //DESIGN PC
    [SerializeField] private float extraTimeJump = 0f;

    [Header("ITEM CONFIGS")]
    [SerializeField] private float armorDuration = 0f;
    [SerializeField] private float bubbleDuration = 0f;

    //GETTER Y SETTER
    public float GetPDpSpeed(){
        return pSpeed;
    }

    public void SetPDpSpeed(float value){
        pSpeed = value;
    }

    public float GetPDjumpPower(){
        return jumpPower;
    }

    public void SetPDjumpPower(float value){
        jumpPower = value;
    }

    public float GetPDmaxJumpForce(){
        return maxJumpForce;
    }

    public void SetPDmaxJumpForce(float value){
        maxJumpForce = value;
    }

    public float GetPDshiftPower(){
        return shiftPower;
    }

    public void SetPDshiftPower(float value){
        shiftPower = value;
    }

    public float GetPDshiftCooldown(){
        return shiftCooldown;
    }

    public void SetPDshiftCooldown(float value){
        shiftCooldown = value;
    }

    public float GetPDfallPower(){
        return fallPower;
    }

    public void SetPDjfallPower(float value){
        fallPower = value;
    }

    public float GetPDfallCooldown(){
        return fallCooldown;
    }

    public void SetPDfallCooldown(float value){
        fallCooldown = value;
    }

    public float GetPDextraTimeJump(){
        return extraTimeJump;
    }

    public void SetPDextraTimeJump(float value){
        extraTimeJump = value;
    }

    public float GetPDarmorDuration(){
        return armorDuration;
    }

    public void SetPDarmorDuration(float value){
        armorDuration = value;
    }

    public float GetPDbubbleDuration(){
        return bubbleDuration;
    }

    public void SetPDbubbleDuration(float value){
        bubbleDuration = value;
    }
}
