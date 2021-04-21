using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba_5
{
    public class Program
    {
        private const string Str = "(a+a)+a#";
        private static Stack<int> _stateStack;
        private static int _index = 0;
        public static void Main(string[] args)
        {
            _stateStack = new Stack<int>();
            _stateStack.Push(0);
            var stack = new Stack<char>();
            var a = Str[_index];
            while (true)
            {
                switch (GetActionType(a, stack))
                {
                    case ActionType.Error:
                        Console.WriteLine("err");
                        return;
                    case ActionType.Shift:
                        break;
                    case ActionType.Reduce:
                        break;
                    case ActionType.Accept:
                        Console.WriteLine("accept");
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                foreach (var s in _stateStack.ToArray().Reverse())
                {
                    Console.Write(s + " ");
                }
                Console.WriteLine();
                a = Str[_index];
            }
        }

        private static ActionType GetActionType(char a, Stack<char> stack)
        {
            switch (a)
            {
                case 'a':
                    switch (_stateStack.Peek())
                    {
                        case 0:
                            _stateStack.Push(3);
                            stack.Push(a);
                            _index++;
                            return ActionType.Shift;
                        case 4:
                            _stateStack.Push(3);
                            stack.Push(a);
                            _index++;
                            return ActionType.Shift;
                        case 5:
                            _stateStack.Push(3);
                            stack.Push(a);
                            _index++;
                            return ActionType.Shift;
                        default:
                            _stateStack.Push(20);
                            stack.Push(a);
                            _index++;
                            return ActionType.Error;
                    }
                case '+':
                    return ActionForPlus(stack, a);
                case '(':
                    switch (_stateStack.Peek())
                    {
                        case 0:
                            _stateStack.Push(4);
                            stack.Push(a);
                            _index++;
                            return ActionType.Shift;
                        case 4:
                            _stateStack.Push(4);
                            stack.Push(a);
                            _index++;
                            return ActionType.Shift;
                        case 5:
                            _stateStack.Push(4);
                            stack.Push(a);
                            _index++;
                            return ActionType.Shift;
                        default:
                            _stateStack.Push(20);
                            return ActionType.Error;
                    }
                case ')':
                    switch (_stateStack.Peek())
                    {
                        case 2:
                            R2(stack);
                            GoTo(stack);
                            return ActionType.Reduce;
                        case 3:
                            R3(stack);
                            GoTo(stack);
                            return ActionType.Reduce;
                        case 6:
                            _stateStack.Push(8);
                            stack.Push(a);
                            _index++;
                            return ActionType.Shift;
                        case 7:
                            R1(stack);
                            GoTo(stack);
                            return ActionType.Reduce;
                        case 8:
                            R4(stack);
                            GoTo(stack);
                            return ActionType.Reduce;
                        default:
                            _stateStack.Push(20);
                            return ActionType.Error;
                    }
                case '#':
                    switch (_stateStack.Peek())
                    {
                        case 1:
                            return ActionType.Accept;
                        case 2:
                            R2(stack);
                            GoTo(stack);
                            return ActionType.Reduce;
                        case 3:
                            R3(stack);
                            GoTo(stack);
                            return ActionType.Reduce;
                        case 7:
                            R1(stack);
                            GoTo(stack);
                            return ActionType.Reduce;
                        case 8:
                            R4(stack);
                            GoTo(stack);
                            return ActionType.Reduce;
                        default:
                            _stateStack.Push(20);
                            return ActionType.Error;
                    }
            }

            return ActionType.Error;
        }

        private static void GoTo(Stack<char> stack)
        {
            var symbolStack = stack.Peek();
            switch (symbolStack)
            {
                case 'E':
                    switch (_stateStack.Peek())
                    {
                        case 0:
                            _stateStack.Push(1);
                            break;
                        case 4:
                            _stateStack.Push(6);
                            break;
                        default:
                            _stateStack.Push(20);
                            Console.WriteLine("errr");
                            break;
                    }
                    break;
                case 'T':
                    switch (_stateStack.Peek())
                    {
                        case 0:
                            _stateStack.Push(2);
                            break;
                        case 4:
                            _stateStack.Push(2);
                            break;
                        case 5:
                            _stateStack.Push(7);
                            break;
                        default:
                            _stateStack.Push(20);
                            Console.WriteLine("errr");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("errr");
                    break;
            }
        }

        private static void R1(Stack<char> stack)
        {
            var reduce = new StringBuilder("");
            for (var i = 1; i < 4; i++)
            {
                _stateStack.Pop();
                reduce.Append(stack.Pop());
            }
            stack.Push('E');
            
            Console.WriteLine($"E -> {reduce}");
        }

        private static void R2(Stack<char> stack)
        {
            _stateStack.Pop();
            
            var symbol = stack.Pop();
            stack.Push('E');
            Console.WriteLine($"E -> {symbol}");
        }

        private static void R3(Stack<char> stack)
        {
            _stateStack.Pop();
            
            var symbol = stack.Pop();
            stack.Push('T');
            Console.WriteLine($"T -> {symbol}");
        }
        
        private static void R4(Stack<char> stack)
        {
            var reduce = new StringBuilder("");
            for (var i = 1; i < 4; i++)
            {
                _stateStack.Pop();
                reduce.Append(stack.Pop());
            }
            stack.Push('T');
            
            Console.WriteLine($"T -> {reduce}");
        }

        private static ActionType ActionForPlus(Stack<char> stack, char a)
        {
            switch (_stateStack.Peek())
            {
                case 1:
                    _stateStack.Push(5);
                    stack.Push(a);
                    _index++;
                    return ActionType.Shift;
                case 2:
                    R2(stack);
                    GoTo(stack);
                    return ActionType.Reduce;
                case 3:
                    R3(stack);
                    GoTo(stack);
                    return ActionType.Reduce;
                case 6:
                    _stateStack.Push(5);
                    stack.Push(a);
                    _index++;
                    return ActionType.Shift;
                case 7:
                    R1(stack);
                    GoTo(stack);
                    return ActionType.Reduce;
                case 8:
                    R4(stack);
                    GoTo(stack);
                    return ActionType.Reduce;
                default:
                    _stateStack.Push(20);
                    return ActionType.Error;
            }
        }
    }
}