using System;
using FightingArena;
using NUnit.Framework;

namespace Tests
{
    public class ArenaTests
    {
        private Arena arena;
        private Warrior warrior1;
        private Warrior attacker;
        private Warrior defender;

        [SetUp]
        public void Setup()
        {
            this.arena = new Arena();
            this.warrior1 = new Warrior("Pesho", 10, 50);

            this.attacker = new Warrior("Pesho", 10, 80);
            this.defender = new Warrior("Gosho", 5, 60);

        }

        [Test]
        public void TestIfConstructorWorksProperly()
        {
            Assert.IsNotNull(this.arena.Warriors);
        }

        [Test]
        public void EnrollShouldPhysicallyAddTheWarriorToTheArena()
        {
            this.arena.Enroll(this.warrior1);

            Assert.That(this.arena.Warriors, Has.Member(this.warrior1));
        }

        [Test]
        public void EnrollShouldIncreaseCount()
        {
            int expectedCount = 2;

            this.arena.Enroll(this.warrior1);
            this.arena.Enroll(new Warrior("Gosho", 5, 60));

            int actualCount = this.arena.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void EnrollTheSameWarriorShouldThrowException()
        {
            this.arena.Enroll(this.warrior1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(this.warrior1);
            });
        }

        [Test]
        public void EnrollTwoWarriorsWithSameNameShouldThrowException()
        {
            Warrior warrior1Copy = new Warrior(warrior1.Name, warrior1.Damage,
                warrior1.HP);

            this.arena.Enroll(this.warrior1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(warrior1Copy);
            });
        }

        [Test]
        public void TestFightingWithMissingAttacker()
        {
            this.arena.Enroll(this.defender);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena
                    .Fight(this.attacker.Name, this.defender.Name);
            });
        }

        [Test]
        public void TestFightingWithMissingDefender()
        {
            this.arena.Enroll(this.attacker);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena
                    .Fight(this.attacker.Name, this.defender.Name);
            });
        }

        [Test]
        public void TestFightingBetweenTwoWarriors()
        {
            //arrange
            int expectedAttackerHealthPoints = this.attacker.HP - this.defender.Damage;
            int expectedDefenderHealthPoints = this.defender.HP - this.attacker.Damage;

            this.arena.Enroll(this.attacker);
            this.arena.Enroll(this.defender);

            //act
            this.arena.Fight(this.attacker.Name, this.defender.Name);

            //assert
            Assert.AreEqual(expectedAttackerHealthPoints, this.attacker.HP);
            Assert.AreEqual(expectedDefenderHealthPoints, this.defender.HP);
        }
    }
}
