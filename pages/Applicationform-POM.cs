using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Miacademy_Playwright.Pages
{
    public class ApplicationFormPage
    {
        private readonly IPage _page;

        public ApplicationFormPage(IPage page)
        {
            _page = page;
        }

public async Task FillParentInformation(string firstName, string lastName, string email, string phone)
{
    // Assert that the values are not null or empty
    if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be null or empty", nameof(firstName));
    if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));
    if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be null or empty", nameof(email));
    if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone cannot be null or empty", nameof(phone));

    // Fill first name
    await _page.GetByRole(AriaRole.Textbox, new() { Name = "Name First Name Required" }).FillAsync(firstName);
    // Fill last name
    await _page.GetByRole(AriaRole.Textbox, new() { Name = "Name Last Name Required" }).FillAsync(lastName);
    // Fill email
    await _page.GetByLabel("Email *").FillAsync(email);
    // Select country code
    await _page.GetByText("+1").ClickAsync();
    await _page.GetByText("Germany (Deutschland)").ClickAsync();
    // Fill phone number
    await _page.GetByLabel("Phone *").FillAsync(phone);

    // Assert and select 'No' option
    var noOption = _page.GetByRole(AriaRole.Combobox, new() { Name = "-Select-" }).Locator("div");
    if (noOption == null) throw new ArgumentException("'No' option element cannot be null", nameof(noOption));
    await noOption.ClickAsync();
    var noOptionItem = _page.GetByRole(AriaRole.Treeitem, new() { Name = "No" });
    if (noOptionItem == null) throw new ArgumentException("'No' option tree item element cannot be null", nameof(noOptionItem));
    await noOptionItem.ClickAsync();
    var noOptionLabel = _page.GetByLabel("No", new() { Exact = true }).GetByText("No");
    if (noOptionLabel == null) throw new ArgumentException("'No' option label element cannot be null", nameof(noOptionLabel));
    await noOptionLabel.ClickAsync();

    // Assert and select 'Other' option
    var otherOptionLabel = _page.GetByLabel("How did you hear about us? (").Locator("label").Filter(new() { HasText = "Other" }).Nth(1);
    if (otherOptionLabel == null) throw new ArgumentException("'Other' option label element cannot be null", nameof(otherOptionLabel));
    await otherOptionLabel.ClickAsync();
    var otherOptionText = _page.GetByLabel("How did you hear about us? (").GetByText("Other", new() { Exact = true });
    if (otherOptionText == null) throw new ArgumentException("'Other' option text element cannot be null", nameof(otherOptionText));
    await otherOptionText.ClickAsync();

    // Assert and click 'What is your preferred start' label
    var preferredStartLabel = _page.GetByLabel("What is your preferred start");
    if (preferredStartLabel == null) throw new ArgumentException("'What is your preferred start' label element cannot be null", nameof(preferredStartLabel));
    await preferredStartLabel.ClickAsync();

    // Select 29th July
    var dateLink = _page.GetByRole(AriaRole.Link, new() { Name = "29" });
    if (dateLink == null) throw new ArgumentException("Date link element cannot be null", nameof(dateLink));
    await dateLink.ClickAsync();
}
        public async Task<bool> AreMandatoryFieldsFilled()
        {
            string parentFirstName = await _page.InputValueAsync("input[name='Name First Name Required']");
            string parentLastName = await _page.InputValueAsync("input[name='Name Last Name Required']");
            string parentEmail = await _page.InputValueAsync("input[name='Email']");
            string parentPhone = await _page.InputValueAsync("input[name='Phone']");
            

            return !string.IsNullOrEmpty(parentFirstName) &&
                   !string.IsNullOrEmpty(parentLastName) &&
                   !string.IsNullOrEmpty(parentEmail) &&
                   !string.IsNullOrEmpty(parentPhone);
                   
        }

        public async Task<bool> IsNoOptionSelected()
        {
            var selectedOption = await _page.GetByRole(AriaRole.Combobox, new() { Name = "-Select-" }).InputValueAsync();
            return selectedOption == "No";
        }

public async Task<bool> IsOtherOptionSelected()
{
    var selectedOption = await _page.GetByLabel("How did you hear about us? (").Locator("label").Filter(new() { HasText = "Other" }).InputValueAsync();
    return !string.IsNullOrEmpty(selectedOption) && selectedOption == "Other";
}
        public async Task<bool> IsPreferredDateSelected(string date)
        {
            var selectedDate = await _page.GetByRole(AriaRole.Link, new() { Name = date }).GetAttributeAsync("class");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return selectedDate.Contains("29");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
