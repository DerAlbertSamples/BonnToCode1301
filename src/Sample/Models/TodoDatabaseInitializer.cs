﻿using System;
using System.Data.Entity;

namespace Sample.Models
{
    // DEMONSTRATION/DEVELOPMENT ONLY
    public class ToDoDatabaseInitializer:
        DropCreateDatabaseIfModelChanges<ToDosContext> // re-creates every time the server starts
        //DropCreateDatabaseIfModelChanges<ToDosContext> 
    {
        protected override void Seed(ToDosContext context)
        {
            SeedDatabase(context);
        }

        public static void SeedDatabase(ToDosContext context)
        {
            _baseCreatedAtDate = new DateTime(2012, 8, 22, 9, 0, 0);

            var todos = new[] {
                // Description, IsDone, IsArchived
                CreateTodo("Food", true, true),
                CreateTodo("Water", true, true),
                CreateTodo("Shelter", true, true),
                CreateTodo("Bread", false, false),
                CreateTodo("Cheese", true, false),
                CreateTodo("Wine", false, false)
           };

            Array.ForEach(todos, t => context.Todos.Add(t));

            context.SaveChanges(); // Save 'em
        }

        private static TodoItem CreateTodo(
            string description, bool isDone, bool isArchived)
        {
            _baseCreatedAtDate = _baseCreatedAtDate.AddMinutes(1);
            return new TodoItem
            {
                CreatedAt = _baseCreatedAtDate,
                Description = description,
                IsDone = isDone,
                IsArchived = isArchived
            };
        }

        private static DateTime _baseCreatedAtDate;

        public static void PurgeDatabase(ToDosContext context)
        {
            var todos = context.Todos;
            foreach (var todoItem in todos)
            {
                todos.Remove(todoItem);
            }

            context.SaveChanges();
        }

    }


}