﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaslavNijinskyOrgApp.Data;
using VaslavNijinskyOrgApp.Models;

namespace VaslavNijinskyOrgApp.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : Controller
    {
        private readonly EventDb _context;

        public ParticipantsController(EventDb context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Participant>> GetAll()
        {
            if (_context.Participant.Any())
            {
                return Ok(_context.Participant);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Participant> GetById(int id)
        {
            if (_context.Participant.Any(p => p.Id == id))
            {
                return Ok(_context.Participant.SingleOrDefault(p => p.Id == id));
            }
            else
            {
                return NotFound($"The database don´t have a participant with the Id {id}");
            }
        }

        [HttpGet("{name}")]
        public ActionResult<Participant> GetByName(string name)
        {
            if (_context.Participant.Any(p => p.Name == name))
            {
                return Ok(_context.Participant.Any(p => p.Name == name));
            }
            else
            {
                return NotFound($"The database don´t have a participant with the name {name}");
            }
        }
        [HttpGet("{schoolName}")]
        public ActionResult<Participant> GetBySchoolName(string schoolName)
        {
            if (_context.Participant.Any(p => p.SchoolName == schoolName))
            {
                return Ok(_context.Participant.Any(p => p.SchoolName == schoolName));
            }
            else
            {
                return NotFound($"The database don´t have a participant with the schoolName {schoolName}");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Participant newParticipant)
        {
            if (!_context.Participant.Any(p => p.Id == newParticipant.Id))
            {
                _context.Participant.Add(newParticipant);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database already has a participant with the Id {newParticipant.Id}");
            }
        }

        [HttpPut]
        public ActionResult Edit([FromBody] Participant newParticipant)
        {
            if (_context.Participant.Any(p => p.Id == newParticipant.Id))
            {
                var ParticipantToUpdate = _context.Participant.Single(p => p.Id == newParticipant.Id);
                _context.Participant.Remove(ParticipantToUpdate);
                _context.Participant.Add(newParticipant);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database don´t have a participant with the Id {newParticipant.Id}");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {

            if (_context.Participant.Any(s => s.Id == id))
            {
                var ParticipantToDelete = _context.Participant.Single(s => s.Id == id);
                _context.Participant.Remove(ParticipantToDelete);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database don´t have a participant with the id {id}");
            }
        }
    }
}
