
using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("bands", allBands);
        model.Add("venues", allVenues);
        return View["index.cshtml", model];
      };
      Post["/"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("bands", allBands);
        model.Add("venues", allVenues);
        return View["index.cshtml", model];
      };
      Get["/venue/{id}"] = param => {
        Venue foundVenue = Venue.Find(param.id);
        List<Band> allBands = foundVenue.GetBands();
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("bands", allBands);
        model.Add("venue", foundVenue);
        return View["venue.cshtml", model];
      };
      Get["/venue/{id}/add"] = param => {
        Venue foundVenue = Venue.Find(param.id);
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("bands", allBands);
        model.Add("venue", foundVenue);
        return View["venue_add_band.cshtml", model];
      };
      Patch["/venue/{id}/add"] = param => {
        Venue foundVenue = Venue.Find(param.id);
        Band addedBand = Band.Find(Request.Form["new-band"]);
        foundVenue.AddBand(addedBand);
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>();
        model.Add("bands", allBands);
        model.Add("venue", foundVenue);
        return View["venue_add_band.cshtml", model];
      };
    }
  }
}
