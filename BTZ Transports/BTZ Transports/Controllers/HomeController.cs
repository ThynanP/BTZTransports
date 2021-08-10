using BTZ_Transports.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTZ_Transports.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                IndexPage model = new IndexPage();
                string userId = Session["usuarioLogadoID"].ToString();
                int id = Int32.Parse(userId);
                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var user = dc.tbUser.Where(a => a.id == id).FirstOrDefault();
                    var abastecimentos = dc.TbAbastecimentos;
                    var carros = dc.TbCarro;
                    var motoristas = dc.TbMotorista;
                    var tipoUser = dc.TbTipoUser.Where(a => a.id == user.tipoId).FirstOrDefault();

                    model.abastecimentos = abastecimentos.ToList();
                    model.carros = carros.ToList();
                    model.motoristas = motoristas.ToList();
                    model.nomeUsuario = user.nome;
                    model.usuario = user.usuario;
                    model.TipoUser = tipoUser;

                }
                    return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login(tbUser u)
        {
            if (ModelState.IsValid)
            {
                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var v = dc.tbUser.Where(a => a.usuario.Equals(u.usuario) && a.senha.Equals(u.senha)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["usuarioLogadoID"] = v.id.ToString();
                        Session["nomeUsuarioLogado"] = v.nome.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(u);
        }
        public ActionResult Logoff()
        {
            Session["usuarioLogadoID"] = null;
            Session["nomeUsuarioLogado"] = null;
            return RedirectToAction("Login");
        }
        #region Motorista
        public ActionResult Motorista()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var motoristas = dc.TbMotorista;
                    List<TbMotorista> model = new List<TbMotorista>();
                    model = motoristas.ToList();

                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public ActionResult MotoristaRegister(int? id)
        {
            if (Session["usuarioLogadoID"] != null)
            {
                if (id != 0 && id != null)
                {
                    using (BTZ_Entities_ dc = new BTZ_Entities_())
                    {
                        var motorista = dc.TbMotorista.Where(a => a.id == id).FirstOrDefault();
                        var status = dc.TbTipoStatus.Where(a => a.id == motorista.statusId).FirstOrDefault();
                        var todosStatus = dc.TbTipoStatus;
                        MotoristaDTO model = new MotoristaDTO();
                        model.id = motorista.id;
                        model.nome = motorista.nome;
                        model.CPF = motorista.CPF;
                        model.CNH = motorista.CNH;
                        model.categoriaCNH = motorista.categoriaCNH;
                        model.nascDate = DateTime.Parse(motorista.nascDate.Date.ToString("yyyy-MM-dd"));
                        model.statusId = motorista.statusId;
                        model.createDate = motorista.createDate;
                        model.updateDate = motorista.updateDate;
                        model.status = status.nome;
                        model.todosStatus = todosStatus.ToList();
                        return View(model);
                    }
                }
                else
                {
                    using (BTZ_Entities_ dc = new BTZ_Entities_())
                    {
                        var todosStatus = dc.TbTipoStatus;
                        MotoristaDTO model = new MotoristaDTO();
                        model.todosStatus = todosStatus.ToList();
                        return View(model);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult SaveMotorista(MotoristaDTO motorista)
        {
           if (motorista.id == 0)
            {
                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var newMotorista = dc.Set<TbMotorista>();
                    newMotorista.Add(new TbMotorista
                    {
                        nome = motorista.nome,
                        CPF = motorista.CPF,
                        CNH = motorista.CNH,
                        categoriaCNH = motorista.categoriaCNH,
                        nascDate = motorista.nascDate,
                        statusId = motorista.statusId,
                        createDate = DateTime.Now,
                        updateDate = DateTime.Now
                    });

                    dc.SaveChanges();
                    return RedirectToAction("Motorista");
                }
            }
            else
            {

                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var query =
                        from m in dc.TbMotorista
                        where m.id == motorista.id
                        select m;
                    foreach (TbMotorista oldmotorista in query)
                    {
                        oldmotorista.id = motorista.id;
                        oldmotorista.nome = motorista.nome;
                        oldmotorista.CPF = motorista.CPF;
                        oldmotorista.CNH = motorista.CNH;
                        oldmotorista.categoriaCNH = motorista.categoriaCNH;
                        oldmotorista.nascDate = Convert.ToDateTime(motorista.nascDate);
                        oldmotorista.statusId = motorista.statusId;
                        oldmotorista.createDate = oldmotorista.createDate;
                        oldmotorista.updateDate = DateTime.Now;
                    }
                    dc.SaveChanges();
                    return RedirectToAction("Motorista");
                }
            }

        }
        public ActionResult DeleteMotorista(int id)
        {
            using (BTZ_Entities_ dc = new BTZ_Entities_())
            {
                TbMotorista motorista = new TbMotorista { id = id };
                dc.Entry(motorista).State = EntityState.Deleted;
                dc.SaveChanges();
                return RedirectToAction("Motorista");
            }
        }
        #endregion
        #region Carros
        public ActionResult Carros()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var carros = dc.TbCarro;
                    var combustivel = dc.TbTipoCombustivel;
                    var fabricante = dc.TbTipoFabricante;
                    CarroView model = new CarroView();
                    model.viewCarros = carros.ToList();
                    model.combustivel = combustivel.ToList();
                    model.fabricante = fabricante.ToList();

                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult CarroRegister(int? id)
        {
            if (Session["usuarioLogadoID"] != null)
            {
                if (id != 0 && id != null)
                {
                    using (BTZ_Entities_ dc = new BTZ_Entities_())
                    {
                        var carro = dc.TbCarro.Where(a => a.id == id).FirstOrDefault();
                        var status = dc.TbTipoStatusCar.Where(a => a.id == carro.statusId).FirstOrDefault();
                        var combustivel = dc.TbTipoCombustivel.Where(a => a.id == carro.idCombustivel).FirstOrDefault();
                        var fabricante = dc.TbTipoFabricante.Where(a => a.id == carro.idFabricante).FirstOrDefault();
                        var todoscombustivel = dc.TbTipoCombustivel;
                        var todosfabricante = dc.TbTipoFabricante;
                        var todosstatus = dc.TbTipoStatusCar;
                        CarroDTO model = new CarroDTO();
                        model.id = carro.id;
                        model.nome = carro.nome;
                        model.placa = carro.placa;
                        model.idCombustivel = carro.idCombustivel;
                        model.idFabricante = carro.idFabricante;
                        model.statusId = carro.statusId;
                        model.ano_fab = carro.ano_fab;
                        model.cap_max = carro.cap_max;
                        model.observacoes = carro.observacoes;
                        model.createDate = carro.createDate;
                        model.updateDate = carro.updateDate;
                        model.combustivel = todoscombustivel.ToList();
                        model.fabricante = todosfabricante.ToList();
                        model.TodosStatus = todosstatus.ToList();
                        return View(model);
                    }
                }
                else
                {
                    using (BTZ_Entities_ dc = new BTZ_Entities_())
                    {
                        var todoscombustivel = dc.TbTipoCombustivel;
                        var todosfabricante = dc.TbTipoFabricante;
                        var todosstatus = dc.TbTipoStatusCar;
                        CarroDTO model = new CarroDTO();
                        model.combustivel = todoscombustivel.ToList();
                        model.fabricante = todosfabricante.ToList();
                        model.TodosStatus = todosstatus.ToList();
                        return View(model);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult SaveCarro(CarroDTO carro)
        {
            if (carro.id == 0)
            {
                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var newcarro = dc.Set<TbCarro>();
                    newcarro.Add(new TbCarro
                    {
                        nome = carro.nome,
                        placa = carro.placa,
                        idCombustivel = carro.idCombustivel,
                        idFabricante = carro.idFabricante,
                        ano_fab = carro.ano_fab,
                        cap_max = carro.cap_max,
                        observacoes = carro.observacoes,
                        statusId = carro.statusId,
                        createDate = DateTime.Now,
                        updateDate = DateTime.Now
                    });

                    dc.SaveChanges();
                    return RedirectToAction("Carros");
                }
            }
            else
            {

                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var query =
                        from c in dc.TbCarro
                        where c.id == carro.id
                        select c;
                    foreach (TbCarro oldCarro in query)
                    {
                        oldCarro.id = carro.id;
                        oldCarro.nome = carro.nome;
                        oldCarro.placa = carro.placa;
                        oldCarro.idCombustivel = carro.idCombustivel;
                        oldCarro.idFabricante = carro.idFabricante;
                        oldCarro.ano_fab = carro.ano_fab;
                        oldCarro.cap_max = carro.cap_max;
                        oldCarro.observacoes = carro.observacoes;
                        oldCarro.statusId = carro.statusId;
                        oldCarro.createDate = oldCarro.createDate;
                        oldCarro.updateDate = DateTime.Now;
                    }
                    dc.SaveChanges();
                    return RedirectToAction("Carros");
                }
            }
        }
        public ActionResult DeleteCarro(int id)
        {
            using (BTZ_Entities_ dc = new BTZ_Entities_())
            {
                TbCarro carro = new TbCarro { id = id };
                dc.Entry(carro).State = EntityState.Deleted;
                dc.SaveChanges();
                return RedirectToAction("Carros");
            }
        }
        #endregion
        #region Abastecimentos
        public ActionResult AbastecimentoRegister(int? id)
        {
            if (Session["usuarioLogadoID"] != null)
            {
                if (id != 0 && id != null)
                {
                    using (BTZ_Entities_ dc = new BTZ_Entities_())
                    {
                        var abastecimento = dc.TbAbastecimentos.Where(a => a.id == id).FirstOrDefault();
                        var carros = dc.TbCarro;
                        var combustiveis = dc.TbTipoCombustivel;
                        var motoristas = dc.TbMotorista;
                        AbastecimentoDTO model = new AbastecimentoDTO();
                        model.id = abastecimento.id;
                        model.idVeiculo = abastecimento.idVeiculo;
                        model.idMotorista = abastecimento.idMotorista;
                        model.idCombustivel = abastecimento.idCombustivel;
                        model.quantidade = abastecimento.quantidade;
                        model.abastecimentoDate = DateTime.Parse(abastecimento.abastecimentoDate.Date.ToString("yyyy-MM-dd"));
                        model.observacoes = abastecimento.observacoes;
                        model.createDate = abastecimento.createDate;
                        model.updateDate = abastecimento.updateDate;
                        model.Total = abastecimento.Total;
                        model.carros = carros.ToList();
                        model.motoristas = motoristas.ToList();
                        model.combustiveis = combustiveis.ToList();
                        return View(model);
                    }
                }
                else
                {
                    using (BTZ_Entities_ dc = new BTZ_Entities_())
                    {
                        var carros = dc.TbCarro;
                        var combustiveis = dc.TbTipoCombustivel;
                        var motoristas = dc.TbMotorista;
                        AbastecimentoDTO model = new AbastecimentoDTO();
                        model.carros = carros.ToList();
                        model.motoristas = motoristas.ToList();
                        model.combustiveis = combustiveis.ToList();
                        return View(model);
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult SaveAbastecimento(AbastecimentoDTO abastecimento)
        {
            double total = 0;
            if (abastecimento.quantidade > 0)
            {
                if(abastecimento.idCombustivel == 1)
                {
                    total = (abastecimento.quantidade * 4.29);
                }
                else if (abastecimento.idCombustivel == 2)
                {
                    total = (abastecimento.quantidade * 2.99);
                }
                else if (abastecimento.idCombustivel == 3)
                {
                    total = (abastecimento.quantidade * 2.09);
                }
            }
            if (abastecimento.id == 0)
            {
                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var newMotorista = dc.Set<TbAbastecimentos>();
                    newMotorista.Add(new TbAbastecimentos
                    {
                        idVeiculo = abastecimento.idVeiculo,
                        idMotorista = abastecimento.idMotorista,
                        abastecimentoDate = abastecimento.abastecimentoDate,
                        idCombustivel = abastecimento.idCombustivel,
                        quantidade = abastecimento.quantidade,
                        observacoes = abastecimento.observacoes,
                        Total = total,
                    createDate = DateTime.Now,
                        updateDate = DateTime.Now
                    });
                    var test = newMotorista;
                    dc.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {

                using (BTZ_Entities_ dc = new BTZ_Entities_())
                {
                    var query =
                        from a in dc.TbAbastecimentos
                        where a.id == abastecimento.id
                        select a;
                    foreach (TbAbastecimentos oldmAbastecimento in query)
                    {
                        oldmAbastecimento.id = abastecimento.id;
                        oldmAbastecimento.idVeiculo = abastecimento.idVeiculo;
                        oldmAbastecimento.idMotorista = abastecimento.idMotorista;
                        oldmAbastecimento.abastecimentoDate = Convert.ToDateTime(abastecimento.abastecimentoDate); ;
                        oldmAbastecimento.idCombustivel = abastecimento.idCombustivel;
                        oldmAbastecimento.quantidade = abastecimento.quantidade;
                        oldmAbastecimento.observacoes = abastecimento.observacoes;
                        oldmAbastecimento.Total = total;
                        oldmAbastecimento.createDate = oldmAbastecimento.createDate;
                        oldmAbastecimento.updateDate = DateTime.Now;
                    }
                    dc.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

        }
        public ActionResult DeleteAbastecimento(int id)
        {
            using (BTZ_Entities_ dc = new BTZ_Entities_())
            {
                TbAbastecimentos abastecimento = new TbAbastecimentos { id = id };
                dc.Entry(abastecimento).State = EntityState.Deleted;
                dc.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        #endregion

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}