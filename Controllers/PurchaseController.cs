using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using exemple.Models;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly PurchaseDao _purchaseDao;

    public PurchaseController(PurchaseDao purchaseDao)
    {
        _purchaseDao = purchaseDao;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
    {
        var purchases = await _purchaseDao.GetPurchases();
        return Ok(purchases);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Purchase>> GetPurchase(int id)
    {
        var purchase = await _purchaseDao.GetPurchase(id);

        if (purchase == null)
        {
            return NotFound();
        }

        return Ok(purchase);
    }

    [HttpPost]
    public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase)
    {
        await _purchaseDao.AddPurchase(purchase);
        return CreatedAtAction("GetPurchase", new { id = purchase.PurchaseId }, purchase);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPurchase(int id, Purchase purchase)
    {
        if (id != purchase.PurchaseId)
        {
            return BadRequest();
        }

        await _purchaseDao.UpdatePurchase(purchase);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePurchase(int id)
    {
        var success = await _purchaseDao.DeletePurchase(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
