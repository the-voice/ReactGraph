﻿using System;
using Shouldly;
using Xunit;

namespace ReactGraph.Tests
{
    public class ApiTest
    {
        [Fact]
        public void FluentApiTest()
        {
            /* Each vertex is a node in this graph
             * 
             *       5<-----4
             *        \    /
             *         \  /
             *          ▼▼
             *      6   1------->3
             *      |  ^  \     ^
             *      | /    \   /
             *      ▼/      ▼ /
             *      0------->2----->7
             *               \
             *                \
             *                 ▼
             *                  8
             */
            var vertex0 = new SinglePropertyType();
            var vertex1 = new SinglePropertyType();
            var vertex2 = new SinglePropertyType();
            var vertex3 = new SinglePropertyType();
            var vertex4 = new SinglePropertyType();
            var vertex5 = new SinglePropertyType();
            var vertex6 = new SinglePropertyType();
            var vertex7 = new SinglePropertyType();
            var vertex8 = new SinglePropertyType();
            var engine = new DependencyEngine();

            engine.Expr(() => Addition(vertex6.Value)).Bind(() => vertex0.Value);
            engine.Expr(() => Addition(vertex0.Value, vertex5.Value, vertex4.Value)).Bind(() => vertex1.Value);
            engine.Expr(() => Addition(vertex0.Value, vertex1.Value)).Bind(() => vertex2.Value);
            engine.Expr(() => Addition(vertex1.Value, vertex2.Value)).Bind(() => vertex3.Value);
            engine.Expr(() => Addition(vertex4.Value)).Bind(() => vertex5.Value);
            engine.Expr(() => Addition(vertex2.Value)).Bind(() => vertex7.Value);
            engine.Expr(() => Addition(vertex2.Value)).Bind(() => vertex8.Value);
                
            Console.WriteLine(engine.ToString());

            // We set the value to 2, then tell the engine the value has changed
            vertex0.Value = 2;
            engine.ValueHasChanged(vertex0, "Value");
            vertex1.Value.ShouldBe(2);
            vertex2.Value.ShouldBe(4);
            vertex3.Value.ShouldBe(6);
        }

        private int Addition(int i1, int i2, int i3)
        {
            return i1 + i2 + i3;
        }

        private int Addition(int i1, int i2)
        {
            return i1 + i2;
        }

        private int Addition(int i1)
        {
            return i1;
        }

        private class SinglePropertyType
        {
            public int Value { get; set; }
        }
    }
}
