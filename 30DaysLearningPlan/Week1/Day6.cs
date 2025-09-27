// 📅 Week-1 Day-6: More OOP – Methods, Properties, Encapsulation
// JavaScript vs C# side-by-side examples

/*
======================================================
1. Private Fields
======================================================
*/

// ✅ JavaScript
/*
class BankAccount {
    #balance; // private field (using #)

    constructor(accountHolder, initialBalance) {
        this.accountHolder = accountHolder;
        this.#balance = initialBalance;
    }
}
*/

// ✅ C#
/*
public class BankAccount
{
    private decimal balance; // private field (C# keyword 'private')

    public BankAccount(string accountHolder, decimal initialBalance)
    {
        AccountHolder = accountHolder;
        balance = initialBalance;
    }
}
*/

/*
======================================================
2. Public Properties
======================================================
*/

// ✅ JavaScript
/*
class BankAccount {
    constructor(accountHolder) {
        this.accountHolder = accountHolder; // public by default
    }
}
*/

// ✅ C#
/*
public class BankAccount
{
    public string AccountHolder { get; set; } // public property
}
*/

/*
======================================================
3. Getters and Setters
======================================================
*/

// ✅ JavaScript
/*
class BankAccount {
    #balance = 0;

    get balance() {
        return this.#balance;  // getter
    }

    set balance(value) {
        if (value >= 0) {
            this.#balance = value;  // setter with validation
        } else {
            console.log("Balance cannot be negative.");
        }
    }
}
*/

// ✅ C#
/*
public class BankAccount
{
    private decimal balance;

    public decimal Balance
    {
        get { return balance; } // getter
        private set             // setter (private for encapsulation)
        {
            if (value >= 0) balance = value;
            else Console.WriteLine("Balance cannot be negative.");
        }
    }
}
*/

/*
======================================================
4. Methods
======================================================
*/

// ✅ JavaScript
/*
class BankAccount {
    #balance = 0;

    deposit(amount) {
        if (amount > 0) {
            this.#balance += amount;
            console.log(`Deposited ${amount}. Balance: ${this.#balance}`);
        }
    }

    withdraw(amount) {
        if (amount > 0 && amount <= this.#balance) {
            this.#balance -= amount;
            console.log(`Withdrew ${amount}. Balance: ${this.#balance}`);
        }
    }
}
*/

// ✅ C#
/*
public class BankAccount
{
    private decimal balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount:C}. Balance: {balance:C}");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            Console.WriteLine($"Withdrew {amount:C}. Balance: {balance:C}");
        }
    }
}
*/

/*
======================================================
5. Encapsulation
======================================================
- Both JavaScript (#private fields) and C# (private keyword + properties)
  hide sensitive data (like balance) and only allow access through
  controlled methods (Deposit, Withdraw).
*/

/*
======================================================
6. Constructor
======================================================
*/

// ✅ JavaScript
/*
class BankAccount {
    #balance;

    constructor(accountHolder, initialBalance) {
        this.accountHolder = accountHolder;
        this.#balance = initialBalance;
    }
}
*/

// ✅ C#
/*
public class BankAccount
{
    private decimal balance;
    public string AccountHolder { get; set; }

    public BankAccount(string accountHolder, decimal initialBalance)
    {
        AccountHolder = accountHolder;
        balance = initialBalance;
    }
}
*/

/*
======================================================
7. Full Example (C# runnable version)
======================================================
*/

using System;

namespace Day6
{
    public class BankAccount
    {
        private decimal balance;

        public string AccountHolder { get; set; }

        public decimal Balance
        {
            get { return balance; }
            private set
            {
                if (value >= 0) balance = value;
                else Console.WriteLine("Balance cannot be negative.");
            }
        }

        public BankAccount(string accountHolder, decimal initialBalance)
        {
            AccountHolder = accountHolder;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Deposited {amount:C}. Balance: {Balance:C}");
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew {amount:C}. Balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine($"Insufficient funds! You only have {Balance:C}.");
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Account Holder: {AccountHolder}");
            Console.WriteLine($"Balance: {Balance:C}.");
        }
    }

    public class Car
    {
        // 🔒 Private field (hidden, like the engine inside the hood)
        private int _speed;

        // Another example: Fuel Level (read-only property, like fuel gauge)
        private int _fuelLevel = 50;    // hidden inside the car

        // 🛠️ Public property with getter & setter (dashboard control)
        public int Speed
        {
            get { return _speed; }  // Getter -> like the speedometer
            set
            {
                if (value >= 0)  // Safety check
                {
                    _speed = value; // Setter -> like pressing the gas pedal
                }
            }
        }

        // FuelLevel (read-only, like the fuel gauge)
        public int FuelLevel
        {
            get { return _fuelLevel; }  // only getter (can read, not set directly)
        }

        // 🚦 Methods (car actions)
        public void StartEngine()
        {
            Console.WriteLine("Engine started.");
        }

        public void Accelerate()
        {
            if (_fuelLevel <= 0)
            {
                Console.WriteLine("Cannot accelerate. out of fuel!");
                return;
            }

            Speed += 10;
            _fuelLevel -= 1;    // using a bit of fuel each acceleration
            Console.WriteLine($"Accelerated. Speed: {Speed} mph. \n Fuel: {_fuelLevel}%");
        }

        public void Brake()
        {
            Speed -= 10;
            if (Speed < 0) Speed = 0;
            Console.WriteLine($"Car accelerated. Current speed: {Speed} mph.");
        }

        public void Honk()
        {
            Console.WriteLine("Beep! Beep!");
        }

        // 🛢️ Encapsulation in action: Refuel safely
        public void Refuel(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid fuel amount.");
                return;
            }

            _fuelLevel += amount;
            if (_fuelLevel > 100) _fuelLevel = 100;
            Console.WriteLine($"Refueled. Fuel level: {_fuelLevel}%");
        }
    }
}

/*
==================== Day 6 Explanations ====================

1. Private Fields:
   - Fields declared as 'private' can only be accessed inside the class.
   - Example: 'private decimal balance;'

2. Public Properties:
   - Used to provide controlled access to private fields.
   - Example: 'public string AccountHolder { get; set; }'

3. Getters and Setters:
   - Getter: allows reading a private field.
   - Setter: allows controlled modification, can include validation.
   - Example:
     public decimal Balance
     {
         get { return balance; }
         private set { if (value >= 0) balance = value; }
     }

4. Methods:
   - Functions inside a class that define behavior.
   - Can access private fields directly.
   - Example: Deposit, Withdraw, DisplayInfo

5. Encapsulation:
   - Combining private fields with public properties and methods
     to control access and protect data.
   - Prevents direct modification of sensitive data like balance.

6. Constructor:
   - Special method that runs when creating an object.
   - Initializes fields and properties.
   - Example: public BankAccount(string accountHolder, decimal initialBalance)

*/