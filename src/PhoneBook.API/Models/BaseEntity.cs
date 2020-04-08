using System;
using System.ComponentModel.DataAnnotations;

public abstract class BaseEntity<T>{
    
        public T Id {get;set;}
        public DateTime CreatedOn{get;set;} = DateTime.Now;
        public int IsDeleted{get;set;}
}