Title: This survey is optional
---
but we would appreciate if you could take a minute to let us know why you are uninstalling, your insights will help the maintainers improve the framework. If you you provide an e-mail address or twitter username, the maintainers may contact you to discuss your feedback and provide assistance where possible.

<script type="text/javascript">
$(document).ready(function()
{
    var getUrlParameter = function getUrlParameter(sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };

    var version = getUrlParameter('version');
    $('#version').val(version);
});
</script>

<form name="uninstall" action="thank-you" class="col-md-12 well" netlify>
    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label for="name">Name</label>
                <input type="text" class="form-control" name="name" placeholder="Full Name">
            </div>
            <div class="form-group">
                <label for="email">Email (optional)</label>
                <input type="email" class="form-control" name="email" placeholder="Email Address">
            </div>
            <div class="form-group">
                <label for="twitter">Twitter (optional)</label>
                <input type="text" class="form-control" name="twitter" placeholder="Twitter Username">
            </div>
            <input id="version" type="hidden" name="version" value="unknown">
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label for="feedback">Feedback</label>
                <textarea class="form-control" name="feedback" rows="11" placeholder="Feedback"></textarea>
            </div>
            <div class="form-group">
                <button class="btn btn-primary pull-right" type="submit">Send</button>
            </div>
        </div>
    </div>
</form>