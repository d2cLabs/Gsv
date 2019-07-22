(function ($) {
    var _$Form = $('#LoginForm');

    function login() {
        abp.ui.setBusy(
            $('#LoginArea'),
            abp.ajax({
                contentType: 'application/x-www-form-urlencoded',
                url: _$Form.attr('action'),
                data: _$Form.serialize(),
            })              
        );
    }

    $('#LoginButton').click(function (e) {
        e.preventDefault();
        if (!_$Form.form('validate'))
            return;

        var tenancyName = $('#TenancyName').val();    
        if (!tenancyName) {
            abp.multiTenancy.setTenantIdCookie(null);
            login();
        }
        else {
            abp.services.app.account.isTenantAvailable({tenancyName: tenancyName})
            .done(function (result) {
                switch (result.state) {
                    case 1: //Available
                        abp.multiTenancy.setTenantIdCookie(result.tenantId);
                        // location.reload();
                        login();
                        break;
                    case 2: //InActive
                        abp.message.warn(abp.utils.formatString(abp.localization
                            .localize("TenantIsNotActive", "Gsv"),
                            tenancyName));
                        break;
                    case 3: //NotFound
                        abp.message.warn(abp.utils.formatString(abp.localization
                            .localize("ThereIsNoTenantDefinedWithName{0}", "Gsv"),
                            tenancyName));
                        break;
                }
            })
        }
    })
})(jQuery);