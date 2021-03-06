﻿using Microsoft.EntityFrameworkCore;
using MrVeggie.Models.Auxiliary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models {

    public class Receita {

        [Key]
        public int id_receita { set; get; }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(50)]
        public string nome { set; get; }

        [Required]
        [Display(Name = "Descrição")]
        [StringLength(500)]
        public string desc { set; get; }

        [Required]
        [Display(Name = "Tempo de Confeção")]
        public float tempo_conf { set; get; }

        [Required]
        [Display(Name = "Avaliação")]
        public float avaliacao { set; get; }

        [Required]
        [Display(Name = "Número de avaliações")]
        public int n_avaliacoes { set; get; }

        [Required]
        [Display(Name = "Dificuldade")]
        public int dificuldade { set; get; }

        [Required]
        [Display(Name = "Calorias")]
        public int calorias { set; get; }

        [Required]
        [Display(Name = "Número de pessoas")]
        public int n_pessoas { set; get; }

        [Required]
        [Display(Name = "Imagem")]
        [StringLength(300)]
        public string url_imagem { set; get; }


        public virtual ICollection<Passo> passos { get; set; }


        [NotMapped]
        public IDictionary<Ingrediente, Quantidade> ingredientes { get; set; }


        [ForeignKey("receita_id")]
        public List<UtilizadorReceitasPref> utilizadores_pref { get; set; }



        [ForeignKey("receita_id")]
        public ICollection<UtensiliosReceita> utensilios_receita { get; set; }

        [NotMapped]
        public IList<Utensilio> utensilios { get; set; }



        [ForeignKey("receita_id")]
        public List<HistoricoUtilizador> historico { get; set; }



        [ForeignKey("receita_id")]
        public List<Agenda> agenda { get; set; }



        public bool Equals(Receita other)
        {
            return null != other && id_receita == other.id_receita;
        }
    }

    public class ReceitaContext : DbContext {

        public ReceitaContext(DbContextOptions<ReceitaContext> options) : base(options) {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UtilizadorReceitasPref>().HasKey(urp => new { urp.utilizador_id, urp.receita_id });

            modelBuilder.Entity<UtensiliosReceita>().HasKey(ut => new { ut.receita_id, ut.utensilio_id });

            modelBuilder.Entity<IngredientesPasso>().HasKey(ip => new { ip.passo_id, ip.ingrediente_id });

            modelBuilder.Entity<UtilizadorIngredientesPref>().HasKey(uip => new { uip.utilizador_id, uip.ingrediente_id });
            modelBuilder.Entity<HistoricoUtilizador>().HasKey(hu => new { hu.utilizador_id, hu.receita_id, hu.data_conf });

            // configures one-to-many relationship
            modelBuilder.Entity<Passo>()
                        .HasOne<Receita>(p => p.receita)     
                        .WithMany(r => r.passos)
                        .HasForeignKey(p => p.receita_id)
                        .HasConstraintName("FKPasso200762");


            


        }



        public DbSet<Receita> Receita { get; set; }
        public DbSet<Passo> Passo { get; set; }
        public DbSet<UtensiliosReceita> UtensiliosReceita { get; set; }
        public DbSet<Utensilio> Utensilio { get; set; }

    }

}


