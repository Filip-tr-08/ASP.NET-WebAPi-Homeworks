using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
using System;
using System.Collections.Generic;


namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private INotesService _noteService;

        public NotesController(INotesService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult<List<NoteDto>> Get()
        {
            try
            {
                return _noteService.GetAllNotes();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetById(int id)
        {
            try
            {
                return _noteService.GetNoteById(id);
            }
            catch(ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddUpdateNoteDto addNoteDto)
        {
            try
            {
                _noteService.AddNote(addNoteDto);
                return StatusCode(StatusCodes.Status201Created, "Note created successfully!");
            }
            catch(NoteException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] AddUpdateNoteDto updateNoteDto)
        {
            try
            {
                _noteService.UpdateNote(updateNoteDto);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (NoteException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }

        [HttpDelete("{id}")] //api/notes
        public IActionResult Delete(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Invalid id value!");
                }
                _noteService.DeleteNote(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(ResourceNotFoundException e)
            {
                //log
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured!");
            }
        }
    }
}
