using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jean_edwards.Database.Entities
{
    public class MovieResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get;set;}
        public string Keyword {get;set;}
        public string SearchResult {get;set;}
        public string ImdbId {get;set;}
        public DateTime DateCreated {get;set;} = DateTime.Now;
    }
}