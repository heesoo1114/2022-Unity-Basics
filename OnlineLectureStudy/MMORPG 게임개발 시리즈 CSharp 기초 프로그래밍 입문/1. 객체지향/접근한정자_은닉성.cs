using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    class Knight
    {
        // 접근한정자
        // public: 공용
        // protected: 자식클래스에서만 사용 가능
        // private: 개인용
        
        public int hp;
        private int attack;
        protected int id;
    }

    class SuperKnight : Knight
    {
        void Test()
        {
            hp = 10; // hp의 접근한정자가 public 이기 때문에 접근가능
            attack = 10; // attack의 접근한정자가 private 이기 때문에 접근불가능
            id = 10; // id의 접근한정자가 protected 이기 때문에 접근가능 (+Knight의 자식클래스이기 때문)
        }
    }

    internal class Test
    {
        static void Main()
        {
            Knight knight = new Knight();
        }
    }
}
