using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;



namespace Heroes
{
    public class Map : IMap
    {
        List<IHero> knights = new List<IHero>();
        List<IHero> barbarians = new List<IHero>();

        public string Fight(ICollection<IHero> players)
        {
            foreach (var item in players)
            {
                if (item is Knight)
                {
                    knights.Add(item);
                }
                else if (item is Barbarian)
                {
                    barbarians.Add(item);
                }
            }


            while (true)
            {
                for (int i = 0; i < knights.Count; i++)
                {
                    if (knights[i].IsAlive != false)
                    {
                        foreach (var barbarian in barbarians)
                        {
                            if (barbarian.IsAlive)
                            {
                                barbarian.TakeDamage(knights[i].Weapon.DoDamage());
                            }
                        }
                    }
                }
                for (int i = 0; i < barbarians.Count; i++)
                {
                    if (barbarians[i].IsAlive != false)
                    {
                        foreach (var item in knights)
                        {
                            if (item.IsAlive)
                            {
                                item.TakeDamage(barbarians[i].Weapon.DoDamage());
                            }

                        }
                    }
                }

                int deadBarb = 0;
                int deadKnights = 0;
                foreach (var barb in barbarians)
                {
                    if (barb.IsAlive == false)
                    {
                        deadBarb++;
                        
                    }
                }
                foreach (var item in knights)
                {
                    if (item.IsAlive == false)
                    {
                        deadKnights++;
                       
                    }
                }
                if (barbarians.Count == deadBarb)
                {
                    return $"The knights took { deadKnights } casualties but won the battle.";
                }
                if (knights.Count == deadKnights)
                {
                    return $"The barbarians took { deadBarb } casualties but won the battle.";
                }
            }
        }
    }
}


           