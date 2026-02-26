using System.Reflection;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Module = ItemChanger.Modules.Module;

namespace ItemChanger.Silksong.Modules
{
    public class SlashModule : Module
    {
        public static SlashModule? Instance { get; private set; }

        public bool HasLeft { get; set; }
        public bool HasRight { get; set; }
        public bool HasUp { get; set; }
        public bool HasDown { get; set; }

        private static readonly MethodInfo? AttackMethod = typeof(HeroController).GetMethod(
            "Attack", BindingFlags.NonPublic | BindingFlags.Instance);

        private static ILHook? attackHook;

        protected override void DoLoad()
        {
            Instance = this;
            if (AttackMethod != null)
            {
                attackHook = new ILHook(AttackMethod, ModifyAttack);
            }
        }

        protected override void DoUnload()
        {
            Instance = null;
            attackHook?.Dispose();
            attackHook = null;
        }

        private static void ModifyAttack(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Ldarg_1);
            cursor.EmitDelegate<Func<HeroController, int, bool>>(ShouldBlockAttack);
            ILLabel continueLabel = cursor.DefineLabel();
            cursor.Emit(OpCodes.Brfalse, continueLabel);
            cursor.Emit(OpCodes.Ret);
            cursor.MarkLabel(continueLabel);
        }

        private enum Direction { Left, Right, Up, Down }

        private static bool ShouldBlockAttack(HeroController hero, int attackDir)
        {
            if (Instance == null) return false;

            Direction dir = GetAttackDirection(hero, attackDir);
            return !CanAttackInDirection(dir);
        }

        private static Direction GetAttackDirection(HeroController hero, int attackDir)
        {
            if (attackDir == 1) return Direction.Up;
            if (attackDir == 2) return Direction.Down;
            return hero.cState.facingRight ? Direction.Right : Direction.Left;
        }

        private static bool CanAttackInDirection(Direction dir)
        {
            if (Instance == null) return true;

            return dir switch
            {
                Direction.Left => Instance.HasLeft,
                Direction.Right => Instance.HasRight,
                Direction.Up => Instance.HasUp,
                Direction.Down => Instance.HasDown,
                _ => true
            };
        }
    }
}
