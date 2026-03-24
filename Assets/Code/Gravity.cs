using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
   Rigidbody rb;
   private const float G = 0.006674f;

   public static List<Gravity> OtherObjectsList;

   private void Awake()
   {
      rb = GetComponent<Rigidbody>();

      if (OtherObjectsList == null)
      {
         OtherObjectsList = new List<Gravity>();
      }
      OtherObjectsList.Add(this);
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
