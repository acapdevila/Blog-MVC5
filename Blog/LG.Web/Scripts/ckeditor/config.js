/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.height = 500;

    config.allowedContent = true;
    config.fillEmptyBlocks = false;

    config.extraPlugins = 'sourcedialog';

    config.toolbar_PorDefecto =
    [
         { name: 'document', items: [/*'Source'*/ 'Sourcedialog', '-', 'DocProps', "-", 'Templates'] },
        { name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] },
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
       // { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
        { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'HorizontalLine', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
        { name: 'styles', items: ['Format', 'FontSize'] },
          '/',
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', '-', 'RemoveFormat'] },
        {
            name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote',
            '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
        },
        { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },

        { name: 'colors', items: ['TextColor', 'BGColor'] }
        //,{ name: 'extra', items: ['Timestamp'] }
    ];

    config.toolbar_Basico =
    [
        ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-', 'About']
    ];

    config.toolbar_Mediano =
   [
   [/*'Source'*/ 'Sourcedialog', '-', 'Bold', 'Italic', 'Underline', '-',
    'Link', 'Unlink', '-', 'HorizontalRule', 'HorizontalLine', '-',
    'Font', 'FontSize', '-', 'Image', '-', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-',

    'Undo', 'Redo']
   ];
};
