using System;
using System.Data;
using System.Collections;

namespace runtest
{
    public class FacialFeatures
    {
        public string EyeColor { get; }
        public decimal PhiltrumWidth { get; }

        public FacialFeatures(string eyeColor, decimal philtrumWidth)
        {
            EyeColor = eyeColor;
            PhiltrumWidth = philtrumWidth;
        }
        // TODO: implement equality and GetHashCode() methods
        public override bool Equals(object obj)
        {
            if (obj is FacialFeatures other)
            {
                return EyeColor == other.EyeColor && PhiltrumWidth == other.PhiltrumWidth;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(EyeColor, PhiltrumWidth);
        }
        
    }

    public class Identity
    {
        public string Email { get; }
        public FacialFeatures FacialFeatures { get; }

        public Identity(string email, FacialFeatures facialFeatures)
        {
            Email = email;
            FacialFeatures = facialFeatures;
        }
        // TODO: implement equality and GetHashCode() methods
        public override bool Equals(object obj)
        {
            if (obj is Identity other)
            {
                return Email == other.Email && FacialFeatures.Equals(other.FacialFeatures);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Email, FacialFeatures);
        }
    }

    public class Authenticator
    {
        private static readonly Identity AdminIdentity = new Identity(
            "admin@exerc.ism",
            new FacialFeatures("green", 0.9m)
        );
        private HashSet<Identity> registeredIdentities = new HashSet<Identity>();

        public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB)
        {
            return faceA.Equals(faceB);
        }

        public bool IsAdmin(Identity identity)
        {
            return identity.Equals(AdminIdentity);
        }

        public bool Register(Identity identity)
        {
            if (IsRegistered(identity))
            {
                return false;
            }
            registeredIdentities.Add(identity);
            return true;
        }

        public bool IsRegistered(Identity identity)
        {
            return registeredIdentities.Contains(identity);
        }

        public static bool AreSameObject(Identity identityA, Identity identityB)
        {
            return ReferenceEquals(identityA, identityB);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            FacialFeatures face1 = new FacialFeatures("blue", 1.2m);
            FacialFeatures face2 = new FacialFeatures("blue", 1.2m);
            Identity identity1 = new Identity("john@example.com", face1);
            Identity identity2 = new Identity("jane@example.com", face2);
            Authenticator authenticator = new Authenticator();
            bool result = authenticator.IsAdmin(identity1);
        }
    }


}
