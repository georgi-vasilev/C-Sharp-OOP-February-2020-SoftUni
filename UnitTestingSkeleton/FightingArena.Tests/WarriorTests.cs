using System;
using FightingArena;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WarriorTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestIfConstructorWorksProperly()
        {
            string expectedName = "Pesho";
            int expectedDamage = 50;
            int expectedHealthPoints = 100;

            Warrior warrior = new Warrior(expectedName, expectedDamage, expectedHealthPoints);

            string actualName = warrior.Name;
            int actualDamage = warrior.Damage;
            int actualHealthPoints = warrior.HP;

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedDamage, actualDamage);
            Assert.AreEqual(expectedHealthPoints, actualHealthPoints);
        }

        [TestCase("          ")]
        [TestCase("")]
        [TestCase(null)]
        public void TestNameValidation(string name)
        {
            int damage = 50;
            int healthPoints = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, healthPoints);
            });
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void TestDamageValidaiton(int damage)
        {
            string name = "Pesho";
            int healthPoints = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, healthPoints);
            });
        }

        [Test]
        public void TestHealthPointsValidation()
        {
            string name = "Pesho";
            int damage = 10;
            int healthPoints = -10;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, healthPoints);
            });
        }

        [Test]
        [TestCase(25)]
        [TestCase(30)]
        public void AttackingEnemyWhenLowHPShouldThrowException(int attackerHealthPoints)
        {
            string attackerName = "Pesho";
            int attackerDamage = 10;

            string defenderName = "Gosho";
            int defenderDamage = 10;
            int defenderHealthPoints = 40;

            Warrior attacker = new Warrior(attackerName, attackerDamage,
                attackerHealthPoints);
            Warrior defender = new Warrior(defenderName, defenderDamage,
                defenderHealthPoints);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        [TestCase(25)]
        [TestCase(30)]
        public void AttackingEnemyWithLowHPShouldThrowException(int defenderHealthPoints)
        {
            string attackerName = "Pesho";
            int attackerDamage = 10;
            int attackerHealthPoints = 100;

            string defenderName = "Gosho";
            int defenderDamage = 10;

            Warrior attacker = new Warrior(attackerName, attackerDamage,
                attackerHealthPoints);
            Warrior defender = new Warrior(defenderName, defenderDamage,
                defenderHealthPoints);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        public void AttackingStrongerEnemyShouldThrowException()
        {
            string attackerName = "Pesho";
            int attackerDamage = 10;
            int attackerHealthPoints = 35;

            string defenderName = "Gosho";
            int defenderDamage = 40;
            int defenderHealthPoints = 35;

            Warrior attacker = new Warrior(attackerName, attackerDamage,
                attackerHealthPoints);
            Warrior defender = new Warrior(defenderName, defenderDamage,
                defenderHealthPoints);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        public void AttackingShouldDecreaseHealthPointsWhenSuccessfull()
        {
            //arrange
            string attackerName = "Pesho";
            int attackerDamage = 10;
            int attackerHealthPoints = 40;

            string defenderName = "Gosho";
            int defenderDamage = 5;
            int defenderHealthPoints = 50;

            Warrior attacker = new Warrior(attackerName, attackerDamage,
                attackerHealthPoints);
            Warrior defender = new Warrior(defenderName, defenderDamage,
                defenderHealthPoints);

            int expectedAttackerHealthPoints = attackerHealthPoints - defenderDamage;
            int expectedDefenderHealthPoints = defenderHealthPoints - attackerDamage;

            //act
            attacker.Attack(defender);

            //assert
            Assert.AreEqual(expectedAttackerHealthPoints, attacker.HP);
            Assert.AreEqual(expectedDefenderHealthPoints, defender.HP);

        }

        [Test]
        public void TestKillingEnemyWithAttack()
        {
            //arrange
            string attackerName = "Pesho";
            int attackerDamage = 80;
            int attackerHealthPoints = 100;

            string defenderName = "Gosho";
            int defenderDamage = 10;
            int defenderHealthPoints = 60;

            Warrior attacker = new Warrior(attackerName, attackerDamage,
                attackerHealthPoints);
            Warrior defender = new Warrior(defenderName, defenderDamage,
                defenderHealthPoints);


            int expectedAttackerHealthPoints = attackerHealthPoints - defenderDamage;
            int expectedDefenderHealthPoints = 0;

            //act
            attacker.Attack(defender);

            //assert
            Assert.AreEqual(expectedAttackerHealthPoints, attacker.HP);
            Assert.AreEqual(expectedDefenderHealthPoints, defender.HP);
        }

    }
}