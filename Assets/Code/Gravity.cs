using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Gravity : MonoBehaviour
{
   Rigidbody rb;
   private const float G = 0.006674f;

   public static List<Gravity> OtherObjectsList;

   [SerializeField]  bool planet = false;
   [SerializeField]  int orbitspeed = 1000;

   private void Awake()
   {
      rb = GetComponent<Rigidbody>();

      if (OtherObjectsList == null)
      {
         OtherObjectsList = new List<Gravity>();
      }
      OtherObjectsList.Add(this);

      if (!planet)
      {
         rb.AddForce(Vector3.left * orbitspeed);
      }
   } 

   private void FixedUpdate()
   {
      foreach (Gravity obj in OtherObjectsList)
      {
         if (obj != this)
         {
            Attract(obj);
         }
      }
   }

   void Attract(Gravity other)
   {
      Rigidbody otherRb = other.rb;

      Vector3 direction = rb.position - otherRb.position;

      float distance = direction.magnitude;

      if (distance == 0f)
      {
         return;
      }

      float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

      Vector3 gravityForce = forceMagnitude * direction.normalized;
      
      otherRb.AddForce(gravityForce);
   }
}
