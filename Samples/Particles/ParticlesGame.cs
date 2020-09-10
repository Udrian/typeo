using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;

namespace Particles
{
    class Particle
    {
        public double Speed { get; set; } = 50;
        public Vec2 Position { get; set; } = Vec2.Zero;
        public Vec2 Velocity { get; set; } = Vec2.Zero;
    }

    class ParticlesGame : Game
    {
        public WindowService WindowService { get; set; }

        public Vec2 ScreenSize { get; set; } = new Vec2(1024, 768);
        public Random Random { get; set; }
        public Window Window { get; set; }
        public Canvas Canvas { get; set; }

        public int ParticlesAmount { get; set; } = 5000;
        public List<Particle> Particles { get; set; }
        public List<Vec2> GravityPoints { get; set; }

        public override void Initialize()
        {
            Random = new Random();
            Particles = new List<Particle>();
            GravityPoints = new List<Vec2>();

            Window = WindowService.CreateWindow("Particles", new Vec2(100, 100), ScreenSize);
            Canvas = WindowService.CreateCanvas(Window);

            for(int i = 0; i < ParticlesAmount; i++)
            {
                Particles.Add(new Particle() { Position = new Vec2(Random.Next(0, (int)ScreenSize.X), Random.Next(0, (int)ScreenSize.Y)) });
            }

            for(int i = 0; i < 6; i++)
            {
                GravityPoints.Add(new Vec2(Random.Next(0, (int)ScreenSize.X), Random.Next(0, (int)ScreenSize.Y)));
            }
        }

        public override void Cleanup()
        {
            Window.Cleanup();
            Canvas.Cleanup();
        }

        public override void Update(double dt)
        {
            for(int i = 0; i < Particles.Count; i++)
            {
                for(int j = 0; j < GravityPoints.Count; j++)
                {
                    var particle = Particles[i];
                    var gravityPoint = GravityPoints[j];

                    var normDir = Vec2.Direction(particle.Position, gravityPoint).Normalize();

                    particle.Velocity += normDir * particle.Speed * dt;
                    particle.Position += particle.Velocity * dt;

                    Particles[i] = particle;
                }
            }
        }

        public override void Draw()
        {
            Canvas.Clear(Color.Black);
            foreach(var particle in Particles)
            {
                var color = Color.Lerp(Color.Cyan, Color.DarkCyan, particle.Velocity.LengthSquared() / 1000);
                Vec2? closestGravityPoint = null;
                foreach(var gravityPoints in GravityPoints)
                {
                    if(closestGravityPoint is null)
                        closestGravityPoint = gravityPoints;
                    else if(closestGravityPoint?.DistanceSquared(particle.Position) > gravityPoints.DistanceSquared(particle.Position))
                    {
                        closestGravityPoint = gravityPoints;
                    }
                }
                color = Color.Lerp(Color.Red, color, Vec2.Distance(particle.Position, closestGravityPoint.Value) / 100);
                Canvas.DrawRectangle(particle.Position, new Vec2(5, 5), true, color);
            }
            Canvas.Present();
        }
    }
}
