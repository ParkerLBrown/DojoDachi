using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

// Your Dojodachi should start with 20 happiness, 20 fullness, 50 energy, and 3 meals.[X]
// After every button press, display a message showing how the Dojodachi reacted
// Feeding your Dojodachi costs 1 meal and gains a random amount of fullness between 5 and 10 
//    (you cannot feed your Dojodachi if you do not have meals)
// Playing with your Dojodachi costs 5 energy and gains a random amount of happiness between 5 and 10
// Every time you play with or feed your dojodachi there should be a 25% chance that it won't like it. Energy or meals will still decrease, but happiness and fullness won't change.
// Working costs 5 energy and earns between 1 and 3 meals
// Sleeping earns 15 energy and decreases fullness and happiness each by 5
// If energy, fullness, and happiness are all raised to over 100, you win! a restart button should be displayed.
// If fullness or happiness ever drop to 0, you lose, and a restart button should be displayed.

namespace DojoDachi
{

  public class HomeController : Controller
  {
    // Requests
    // localhost:5000/
    // [Route("")]
    [HttpGet("")] // can combine these two lines into one
    public ViewResult Index()
    {
      HttpContext.Session.SetInt32("fullness", 20);
      HttpContext.Session.SetInt32("happiness", 20);
      HttpContext.Session.SetInt32("meals", 3);
      HttpContext.Session.SetInt32("energy", 50);
      ViewBag.Action = "Hi Dojodachi, what do you want to do!?";
      ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
      ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
      ViewBag.Meals = HttpContext.Session.GetInt32("meals");
      ViewBag.Energy = HttpContext.Session.GetInt32("energy");
      ViewBag.Img = Url.Content("~/img/Dojodachi.jpg");
      return View();
    }

    [HttpPost("feed")]
    public IActionResult Feed(int min, int max)
    {
      if(HttpContext.Session.GetInt32("meals") < 1)
      {
        string cannot = "Not enough meals! Work to earn some meals first!";
        ViewBag.Cannot = cannot;
        ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
        ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
        ViewBag.Meals = HttpContext.Session.GetInt32("meals");
        ViewBag.Energy = HttpContext.Session.GetInt32("energy");
        return View("Index");
      }
      else
      {
        int? meals = HttpContext.Session.GetInt32("meals");
        meals -= 1;
        HttpContext.Session.SetInt32("meals", (int)meals);
        ViewBag.StatTwo = "-1 meal";
        Random rand = new Random();
        int chance = rand.Next(0, 101);
        if (chance > 25) 
        {
          string Feed = "You fed your Dojodachi";
          ViewBag.Action = Feed;
          int? fullness = HttpContext.Session.GetInt32("fullness");
          int fed = rand.Next(min, max);
          fullness += fed;
          ViewBag.StatOne = $"+{fed} fullness";
          HttpContext.Session.SetInt32("fullness", (int)fullness);
          ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
          ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
          ViewBag.Meals = HttpContext.Session.GetInt32("meals");
          ViewBag.Energy = HttpContext.Session.GetInt32("energy");
          return View("Index");
        }
        else
        {
          string notlike = "Your Dojodachi didn't like that food! No gain to fullness!";
          ViewBag.NotLike = notlike;
          ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
          ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
          ViewBag.Meals = HttpContext.Session.GetInt32("meals");
          ViewBag.Energy = HttpContext.Session.GetInt32("energy");
          return View("Index");
        }
      }
    }

    [HttpPost("play")]
    public IActionResult Play(int min, int max)
    {
      if(HttpContext.Session.GetInt32("energy") < 1)
      {
        string cannot = "Dojodachi is tired! Sleep to recharge energy!";
        ViewBag.Cannot = cannot;
        ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
        ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
        ViewBag.Meals = HttpContext.Session.GetInt32("meals");
        ViewBag.Energy = HttpContext.Session.GetInt32("energy");
        return View("Index");
      }
      else
      {
        int? energy = HttpContext.Session.GetInt32("energy");
        energy -= 5;
        if(energy < 0)
          {
            energy = 0;
          }
        HttpContext.Session.SetInt32("energy", (int)energy);
        ViewBag.StatTwo = "-5 energy";
        Random rand = new Random();
        int chance = rand.Next(0, 101);
        if (chance > 25) 
        {
          string Play = "You played with your Dojodachi!";
          ViewBag.Action = Play;
          int? happiness = HttpContext.Session.GetInt32("happiness");
          int play = rand.Next(min, max);
          happiness += play;
          ViewBag.StatOne = $"+{play} happiness";
          HttpContext.Session.SetInt32("happiness", (int)happiness);
          ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
          ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
          ViewBag.Meals = HttpContext.Session.GetInt32("meals");
          ViewBag.Energy = HttpContext.Session.GetInt32("energy");
          return View("Index");
        }
        else
        {
          string notlike = "Your Dojodachi didn't like that game! No gain to Happiness!";
          ViewBag.NotLike = notlike;
          ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
          ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
          ViewBag.Meals = HttpContext.Session.GetInt32("meals");
          ViewBag.Energy = HttpContext.Session.GetInt32("energy");
          return View("Index");
        }
      }
    }

    [HttpPost("work")]
    public IActionResult Work(int min, int max)
    {
      if(HttpContext.Session.GetInt32("energy") < 1)
      {
        string cannot = "Dojodachi is tired! Sleep to recharge energy!";
        ViewBag.Cannot = cannot;
        ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
        ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
        ViewBag.Meals = HttpContext.Session.GetInt32("meals");
        ViewBag.Energy = HttpContext.Session.GetInt32("energy");
        return View("Index");
      }
      else
      {
        Random rand = new Random();
        string Work = "You sent your Dojodachi to work!";
        ViewBag.Action = Work;
        int? energy = HttpContext.Session.GetInt32("energy");
        energy -= 5;
        if(energy < 0)
          {
            energy = 0;
          }
        int? meals = HttpContext.Session.GetInt32("meals");
        int work = rand.Next(min, max);
        meals += work;
        ViewBag.StatOne = $"+{work} meals";
        ViewBag.StatTwo = "-5 energy";
        HttpContext.Session.SetInt32("meals", (int)meals);
        HttpContext.Session.SetInt32("energy", (int)energy);
        ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
        ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
        ViewBag.Meals = HttpContext.Session.GetInt32("meals");
        ViewBag.Energy = HttpContext.Session.GetInt32("energy");
        return View("Index");
      }
    }

    [HttpPost("sleep")]
    public IActionResult Sleep()
    {
      if(HttpContext.Session.GetInt32("happiness") < 1 && HttpContext.Session.GetInt32("fullness") < 1)
      {
        string cannot = "Dojodachi is unhappy and hungry! Feed and make your Dojodachi happier to sleep!";
        ViewBag.Cannot = cannot;
        ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
        ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
        ViewBag.Meals = HttpContext.Session.GetInt32("meals");
        ViewBag.Energy = HttpContext.Session.GetInt32("energy");
        return View("Index");
      }
      if(HttpContext.Session.GetInt32("happiness") < 1)
      {
        string cannot = "Dojodachi is unhappy! Make your Dojodachi happier to sleep!";
        ViewBag.Cannot = cannot;
        ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
        ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
        ViewBag.Meals = HttpContext.Session.GetInt32("meals");
        ViewBag.Energy = HttpContext.Session.GetInt32("energy");
        return View("Index");
      }
      if(HttpContext.Session.GetInt32("fullness") < 1)
      {
        string cannot = "Dojodachi is hungry! Feed your Dojodachi to sleep!";
        ViewBag.Cannot = cannot;
        ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
        ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
        ViewBag.Meals = HttpContext.Session.GetInt32("meals");
        ViewBag.Energy = HttpContext.Session.GetInt32("energy");
        return View("Index");
      }
      else
      {
        string sleep = "Your Dojodachi went to sleep!";
        ViewBag.Action = sleep;
        int? energy = HttpContext.Session.GetInt32("energy");
        energy += 15;
        if(energy > 100)
          {
            energy = 100;
          }
        int? happiness = HttpContext.Session.GetInt32("happiness");
        int? fullness = HttpContext.Session.GetInt32("fullness");
        happiness -= 5;
        fullness -= 5;
        if(happiness < 0)
          {
            happiness = 0;
          }
        if(fullness < 0)
          {
            fullness = 0;
          }
        ViewBag.StatOne = $"+15 energy";
        ViewBag.StatTwo = "-5 happiness -5 fullness";
        HttpContext.Session.SetInt32("energy", (int)energy);
        HttpContext.Session.SetInt32("happiness", (int)happiness);
        HttpContext.Session.SetInt32("fullness", (int)fullness);
        ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
        ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
        ViewBag.Meals = HttpContext.Session.GetInt32("meals");
        ViewBag.Energy = HttpContext.Session.GetInt32("energy");
        return View("Index");
      }
    }

    [HttpPost("restart")]
    public RedirectToActionResult Restart()
    {
      HttpContext.Session.Clear();
      return RedirectToAction ("Index");
    }
  }
}